# Ant Path Matching
[![Build status](https://ci.appveyor.com/api/projects/status/dh8kgx9rooyx6i40?svg=true)](https://ci.appveyor.com/project/WichardRiezebos/ant-path-matching) [![NuGet](https://buildstats.info/nuget/AntPathMatching)](https://www.nuget.org/packages/AntPathMatching/) [![Join the chat at https://gitter.im/ant-path-matching/Lobby](https://badges.gitter.im/ant-path-matching/Lobby.svg)](https://gitter.im/ant-path-matching/Lobby?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge&utm_content=badge)

Package for matching paths (files, directories) using the apache ant-style.

## Prerequisites

- .NET Framework 2.0

## Installation

Install the NuGet package using the command below:

```
Install-Package AntPathMatching
```

...or search for "`AntPathMatching`" in the NuGet index.

## Getting started
The code below is an example how to use the library.

#### Standalone match

```
using AntPathMatching;
...
var ant = new Ant("/assets/**/*.{js,css}");
var isMatch = ant.IsMatch("/assets/scripts/vendor/angular.js");
```

#### Recursive match

```
using AntPathMatching;
...
var ant = new Ant("/assets/**/*.js");
var antDir = new AntDirectory(ant);
var matchingFiles = ant.SearchRecursively("C:\directory\", includeDirectoryPath: false);
```

#### Dependency Injection

```
using AntPathMatching;
...
constructor(
	IAntFactory antFactory,				
	IAntDirectoryFactory antDirectoryFactory
) {
	var ant = antFactory.CreateNew("/assets/**/*.js");
	var antDir = antDirectoryFactory.CreateNew(ant);
}
```