## Harmony

Harmony is a tool that will allow you to edit aliases in-game in real time from your csv files.

## Using Harmony

You'll need the x64 .NET Core 3.1 runtime before running Harmony, available [Here](https://dotnet.microsoft.com/download).

To use Harmony simply run it while a map/mod is running, and make eny edits you want to csv/template files in the aliases or templates folder. Any changes to files in those the aliases and templates folder will trigger an update for Harmony to reload them.

Harmony can be used with tools such as Microsoft Office or Open Office which lock files as it opens them in sharing mode, it does not care if the file/s are open in other programs.

Harmony will by default check if a mod is running before attempting any overrides, this is to avoid it running outside a mod and potentially triggering any anti-cheat in the game.

## Filtering

By default Harmony will simply parse all aliases, resolve templates, and then override any aliases from the aliases folder that are loaded in-game. In most cases this can slow down Harmony as it attempts to find aliases to override, and usually you probably only want to edit one or 2 csv files.

To tell Harmony which csv files in the aliases folder to parse from, simply pass them as command line arguments, for example:

`Harmony.exe my_weapons_aliases.csv my_amb_aliases.csv`

## License/Disclaimer

Please read the License.txt file in the download before using this.

THE SOFTWARE IS PROVIDED “AS IS”, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.