//  ------------------------------------------------------------------------------------
//  Copyright(c) 2020 Philip/Scobalula
//  ------------------------------------------------------------------------------------
//  Permission is hereby granted, free of charge, to any person obtaining a copy of this
//  software and associated documentation files (the "Software"), to deal in the Software
//  without restriction, including without limitation the rights to use, copy, modify,
//  merge, publish, distribute, sublicense, and/or sell copies of the Software, and to
//  permit persons to whom the Software is furnished to do so, subject to the following
//  conditions:
//  ------------------------------------------------------------------------------------
//  The above copyright notice and this permission notice shall be included in all copies
//  or substantial portions of the Software.
//  ------------------------------------------------------------------------------------
//  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED,
//  INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A
//  PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
//  HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF
//  CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE
//  OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//  ------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using PhilLibX.IO;

namespace Harmony
{
    /// <summary>
    /// Base Instance Class
    /// </summary>
    internal static class Instance
    {
        /// <summary>
        /// Black Ops III Asset Pool Structure
        /// </summary>
        [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
        public struct AssetPool
        {
            public long PoolPointer;
            public int SizeOfHeader;
            public int Capacity;
            public int Padding;
            public int Count;
            public long FreeHeader;
        }

        /// <summary>
        /// Gets the Alias File Name List for overriding
        /// </summary>
        public static List<string> AliasFileNames { get; } = new List<string>();

        /// <summary>
        /// Gets or Sets the Loaded Banks
        /// </summary>
        public static List<SoundBank> Banks { get; } = new List<SoundBank>();

        /// <summary>
        /// Gets or Sets the Source Bank
        /// </summary>
        public static SoundBank SourceBank { get; set; }

        /// <summary>
        /// Gets or Sets the Template Bank
        /// </summary>
        public static SoundBank TemplateBank { get; set; }

        /// <summary>
        /// Gets or Sets Black Ops III's Process
        /// </summary>
        public static ForeignProcess BlackOpsIII { get; set; }

        /// <summary>
        /// Gets or Sets the Asset Pools Address
        /// </summary>
        public static long AssetPoolsAddress { get; set; }

        /// <summary>
        /// Gets or Sets the Asset Pools
        /// </summary>
        public static AssetPool[] AssetPools { get; set; }

        /// <summary>
        /// Gets or Sets the address of the name of the mod
        /// </summary>
        public static long ModNameAddress { get; set; }

        /// <summary>
        /// Gets or Sets Black Ops III's Folder
        /// </summary>
        public static string BlackOpsIIIFolder { get; set; }

        /// <summary>
        /// Gets or Sets the Template Folder Watcher
        /// </summary>
        public static FileSystemWatcher SoundFolderWatcher { get; set; }

        /// <summary>
        /// Gets the Raw Folder
        /// </summary>
        public static string RawFolder
        {
            get
            {
                return Path.Combine(BlackOpsIIIFolder, "share", "raw");
            }
        }

        /// <summary>
        /// Gets the Sound Folder
        /// </summary>
        public static string SoundFolder
        {
            get
            {
                return Path.Combine(RawFolder, "sound");
            }
        }

        /// <summary>
        /// Gets the Sound Templates Folder
        /// </summary>
        public static string SoundTemplatesFolder
        {
            get
            {
                return Path.Combine(SoundFolder, "templates");
            }
        }

        /// <summary>
        /// Gets the Sound Aliases Folder
        /// </summary>
        public static string SoundAliasesFolder
        {
            get
            {
                return Path.Combine(SoundFolder, "aliases");
            }
        }

        /// <summary>
        /// Initializes Harmony
        /// </summary>
        public static void Initialize()
        {
            var process = Process.GetProcessesByName("blackops3");

            // If we couldn't find it, we dip
            if (process.Length <= 0)
                throw new Exception("Failed to locate Black Ops III's process. If the game is running, try running Harmony as admin");

            BlackOpsIII = new ForeignProcess(process[0]);

            var module = BlackOpsIII.Modules[0];

            var pools = BlackOpsIII.FindBytes(
                new byte?[] { 0x63, 0xC1, 0x48, 0x8D, 0x05, null, null, null, null, 0x49, 0xC1, 0xE0, null, 0x4C, 0x03, 0xC0 },
                module.BaseAddress.ToInt64(),
                module.BaseAddress.ToInt64() + module.Size,
                true);
            var getModNameFunctionScan = BlackOpsIII.FindBytes(
                new byte?[] { 0x00, 0xE8, null, null, null, 0xFF, 0x84, 0xC0, 0x74, 0x4F, 0xE8, null, null, null, 0xFF, 0x84 },
                module.BaseAddress.ToInt64(),
                module.BaseAddress.ToInt64() + module.Size,
                true);

            // If we couldn't find it, we dip
            if (pools.Length <= 0 && getModNameFunctionScan.Length <= 0)
                throw new Exception("Failed to locate the required information in Black Ops III's memory");

            // Parse the address of the mod name
            var getModNameFunctionAddress = BlackOpsIII.ReadInt32(getModNameFunctionScan[0] + 20) + getModNameFunctionScan[0] + 24;
            ModNameAddress = BlackOpsIII.ReadInt32(getModNameFunctionAddress + 3) + getModNameFunctionAddress + 7;

            // Set directory
            BlackOpsIIIFolder = module.FileDirectory;

            // We require the mod name specifically to ensure we don't run Harmony while a mod is not running
            // potentially triggering VAC and getting the person banned
            if (string.IsNullOrWhiteSpace(BlackOpsIII.ReadNullTerminatedString(ModNameAddress)))
                throw new Exception("No mod is currently loaded.");

            Console.WriteLine("> Loaded Mod: {0}", BlackOpsIII.ReadNullTerminatedString(ModNameAddress));

            // Set the required data
            AssetPoolsAddress = BlackOpsIII.ReadInt32(pools[0] + 0x5) + (pools[0] + 0x9);
            AssetPools = BlackOpsIII.ReadArrayUnsafe<Instance.AssetPool>(AssetPoolsAddress, 107);


            var soundPool = AssetPools[10];

            var stopWatch = Stopwatch.StartNew();

            foreach (var header in BlackOpsIII.ReadArray<SoundBank.SoundBankAsset>(soundPool.PoolPointer, soundPool.Count))
            {
                // Skip if no aliases
                if (header.AliasesPointer == 0 && header.AliasCount == 0)
                    continue;

                Banks.Add(new SoundBank(BlackOpsIII.ReadNullTerminatedString(header.NamePointer), header));
            }

            Console.WriteLine("> Parsed {0} sound banks from Black Ops III's memory in {1} seconds", Banks.Count, stopWatch.ElapsedMilliseconds / 1000.0f);

            // Create template watcher
            SoundFolderWatcher = new FileSystemWatcher
            {
                Path                  = SoundFolder,
                NotifyFilter          = NotifyFilters.LastAccess | NotifyFilters.LastWrite | NotifyFilters.FileName | NotifyFilters.DirectoryName,
                Filter                = "*.csv",
                EnableRaisingEvents   = true,
                IncludeSubdirectories = true,
            };

            // Add events
            SoundFolderWatcher.Changed += new FileSystemEventHandler(OnSoundFolderChanged);
            SoundFolderWatcher.Created += new FileSystemEventHandler(OnSoundFolderChanged);
            SoundFolderWatcher.Deleted += new FileSystemEventHandler(OnSoundFolderChanged);
            SoundFolderWatcher.Renamed += new RenamedEventHandler(OnSoundFolderRenamed);
        }

        public static void ParseTemplates()
        {
            TemplateBank = new SoundBank("Template");

            foreach(var file in Directory.EnumerateFiles(SoundTemplatesFolder, "*.csv", SearchOption.AllDirectories))
            {
                Console.WriteLine("> Parsing {0}", file);
                string[] header = null;

                try
                {
                    foreach (var line in CSV.LoadFile(file))
                    {
                        if (header == null)
                        {
                            header = line;
                        }
                        else
                        {
                            var result = new SoundAlias(line, header, TemplateBank);

                            // For 4 iq raptroes we must validate
                            if (!string.IsNullOrWhiteSpace(result.Name))
                                TemplateBank.GetOrAddAlias(result.Name).Add(result);
                        }
                    }
                }
                catch { }
            }
        }


        public static void ParseAliases()
        {
            SourceBank = new SoundBank("Source");

            foreach (var file in Directory.EnumerateFiles(SoundAliasesFolder, "*.csv", SearchOption.AllDirectories))
            {
                if (IsAliasFileValid(Path.GetFileNameWithoutExtension(file)))
                {
                    Console.WriteLine("> Parsing {0}", file);

                    string[] header = null;

                    try
                    {
                        foreach (var line in CSV.LoadFile(file))
                        {
                            if (header == null)
                            {
                                header = line;
                            }
                            else
                            {
                                var result = new SoundAlias(line, header, SourceBank);

                                // For 4 iq raptroes we must validate
                                if (!string.IsNullOrWhiteSpace(result.Name))
                                    SourceBank.GetOrAddAlias(result.Name).Add(result);
                            }
                        }
                    }
                    catch(Exception e)
                    {
                        PrintException("Parse Error", e);
                    }
                }
            }
        }

        /// <summary>
        /// Overrides aliases loaded in-game with those from the source bank
        /// </summary>
        public static void OverrideAliases()
        {
            foreach (var alias in SourceBank.Aliases)
            {
                bool found = false;

                foreach (var loadedBank in Banks)
                {
                    if (loadedBank.Aliases.TryGetValue(alias.Key, out var loadedList))
                    {
                        Console.WriteLine("> Overriding {0}", alias.Key);

                        // Validate count
                        if (loadedList.Count != alias.Value.Count)
                            Console.WriteLine("> WARNING: Entry count mismatch, In-Game count: {0} Source count: {1}", loadedList.Count, alias.Value.Count); ;

                        found = true;

                        for (int i = 0; i < loadedList.Count; i++)
                        {
                            loadedList[i].DeserializeFromBo3();
                            loadedList[i].CopyFrom(alias.Value[i < alias.Value.Count ? i : alias.Value.Count - 1]);
                            loadedList[i].SerializeToBo3();
                        }

                        // Found
                        break;
                    }
                }

                if (!found)
                    Console.WriteLine("> Failed to find {0}", alias.Key);
            }
        }

        /// <summary>
        /// Prints an exception (full exception in debug or simple message in release)
        /// </summary>
        public static void PrintException(string message, Exception e)
        {
#if DEBUG
            Console.WriteLine("> {0}: {1}", message, e.ToString());
#else
            Console.WriteLine("> {0}: {1}", message, e.Message);
#endif
        }

        /// <summary>
        /// Triggers reloading when a file is edited in the sound folder
        /// </summary>
        public static void OnSoundFolderChanged(object source, FileSystemEventArgs e)
        {
            Console.WriteLine("> Changed detected, updating aliases...");
            // Validate if a mod is still running
            if (string.IsNullOrWhiteSpace(BlackOpsIII.ReadNullTerminatedString(ModNameAddress)))
                throw new Exception("No mod is currently loaded.");
            ParseTemplates();
            ParseAliases();
            OverrideAliases();
            Console.WriteLine("> Watching for changes....");
        }

        /// <summary>
        /// Triggers reloading when a file is edited in the sound folder
        /// </summary>
        public static void OnSoundFolderRenamed(object source, RenamedEventArgs e)
        {
            Console.WriteLine("> Changed detected, updating aliases...");
            // Validate if a mod is still running
            if (string.IsNullOrWhiteSpace(BlackOpsIII.ReadNullTerminatedString(ModNameAddress)))
                throw new Exception("No mod is currently loaded.");
            ParseTemplates();
            ParseAliases();
            OverrideAliases();
            Console.WriteLine("> Watching for changes....");
        }

        /// <summary>
        /// Checks if the alias file is valid and should be parsed
        /// </summary>
        public static bool IsAliasFileValid(string fileName)
        {
            if (AliasFileNames.Count == 0)
                return true;

            return AliasFileNames.Contains(fileName.ToLower());
        }
    }
}
