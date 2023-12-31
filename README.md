# Code katas (copied from [kata-bootstraps](https://github.com/swkBerlin/kata-bootstraps/tree/master/csharp/dotnet6))

## Development

### What is inside?

- Logic - is a DLL project for business logic
- Logic.Tests - is the corresponding xUnit test project, configured with static code rules allowing
  underscores in test names (GlobalSuppressions.cs)

### Prerequisites

To compile, test and run this project the latest [.NET Core SDK](https://dotnet.microsoft.com/download) is required on
your machine. For calculating code metrics I recommend [metrix++](https://github.com/metrixplusplus/metrixplusplus).
This requires python.

If you are interested in test coverage, then you'll need the following tools installed:

```shell
dotnet tool install --global coverlet.console
dotnet tool install --global dotnet-reportgenerator-globaltool
```

## Build, Test, Run

Run the following commands from the folder containing the `.sln` file in order to build and test.

### Build the Solution and Run the Tests

```shell
dotnet build
dotnet test
```
```shell
# If you like continuous testing then use the dotnet file watcher to trigger your tests
dotnet watch --project ./tests/Logic.UnitTests test
```
```shell
# As an alternative, run the tests with coverage and produce a coverage report
rm -r tests/Logic.UnitTests/TestResults && \
  dotnet test --no-restore --verbosity normal /p:CollectCoverage=true /p:CoverletOutputFormat=cobertura /p:CoverletOutput='./TestResults/coverage.cobertura.xml' && \
  reportgenerator "-reports:tests/Logic.UnitTests/TestResults/*.xml" "-targetdir:report" "-reporttypes:Html;lcov" "-title:DotnetStarter"
open report/index.html
```

### Apply code formatting rules

```shell
dotnet format
```

### Check Code Metrics

... check code metrics using [metrix++](https://github.com/metrixplusplus/metrixplusplus)

- Configure the location of the cloned metrix++ scripts
  ```shell
  export METRIXPP=/path/to/metrixplusplus
  ```

- Collect metrics
  ```shell
  python "$METRIXPP/metrixpp.py" collect --std.code.complexity.cyclomatic --std.code.lines.code --std.code.todo.comments --std.code.maintindex.simple -- .
  ```

- Get an overview
  ```shell
  python "$METRIXPP/metrixpp.py" view --db-file=./metrixpp.db
  ```

- Apply thresholds
  ```shell
  python "$METRIXPP/metrixpp.py" limit --db-file=./metrixpp.db --max-limit=std.code.complexity:cyclomatic:5 --max-limit=std.code.lines:code:25:function --max-limit=std.code.todo:comments:0 --max-limit=std.code.mi:simple:1
  ```

At the time of writing, I want to stay below the following thresholds:

```text
--max-limit=std.code.complexity:cyclomatic:5
--max-limit=std.code.lines:code:25:function
--max-limit=std.code.todo:comments:0
--max-limit=std.code.mi:simple:1
```

### Remove Code Duplication Where Appropriate

To detect duplicates I use the [CPD Copy Paste Detector](https://pmd.github.io/latest/pmd_userdocs_cpd.html)
tool from the [PMD Source Code Analyzer Project](https://pmd.github.io/latest/index.html).

If you have installed PMD by download & unzip, replace `pmd` by `./run.sh`.
The [homebrew pmd formula](https://formulae.brew.sh/formula/pmd) makes the `pmd` command globally available.

```sh
pmd cpd --minimum-tokens 50 --language cs --files .
```
