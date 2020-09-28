# example-percy-csharp-selenium
This example app is an HTTP server that serves a fork of the [TodoMVC](https://github.com/tastejs/todomvc)
[Vanilla-ES6](https://github.com/tastejs/todomvc/tree/master/examples/vanilla-es6)
(forked at commit
[c78ae12a1834a11da6236c64a0c0fb06b20b7c51](https://github.com/tastejs/todomvc/tree/c78ae12a1834a11da6236c64a0c0fb06b20b7c51)).

The project requires .NET Core 3.1 or higher. 

On Mac OS, you can use Homebrew:
```bash
$ brew cask install dotnet
```

The Selenium tests use ChromeDriver, which you need to install separately for your system.

On Mac OS, you can use Homebrew:
```bash
$ brew tap homebrew/cask && brew cask install chromedriver
```

On Windows, you can use Chocolatey:

```bash
$ choco install chromedriver
```

For other systems (or installation alternatives), see:
https://github.com/SeleniumHQ/selenium/wiki/ChromeDriver

## Building and running the app

Option 1: In case you'd like to use node to build the code and trigger the test, simply run the below command. Please ensure to supply a valid PERCY_TOKEN with the command.

```bash
$ npm install
$ PERCY_TOKEN=<YOUR_PROJECT_TOKEN> npm run test
```

Option 2: In case you have already installed @percy/agent node module globally on your machine.

To compile and build a .dll file containing our app:
```bash
$ dotnet build
```

Then, to run the tests and create the build on Percy:
```bash
$ PERCY_TOKEN=<YOUR_PROJECT_TOKEN> percy exec -- dotnet example-percy-csharp-selenium/bin/Debug/*/example-percy-csharp-selenium.dll
```