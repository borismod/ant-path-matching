# Ant Path Matching
 
Package for matching paths (files, directories) using the apache ant-style.

| | |
| --- | --- |
| **Build** | ? |
| **NuGet** | [![NuGet](https://buildstats.info/nuget/AntPathMatching)](https://www.nuget.org/packages/AntPathMatching/) |
| **Gitter** | ? |

## Installation

Install the NuGet package using the command below,

```
Install-Package AntPathMatching
```

. . . or search for `AntPathMatching` in the NuGet index.

## Getting started
The code below is an example how to use the library.

# Standalone

```
var ant = new Ant("/assets/**/*.js");
var isMatch = ant.IsMatch("/assets/scripts/vendor/angular.js");
```

# Directory

```
var ant = new Ant("/assets/**/*.js");
var antDir = new AntDirectory(ant);
var matchingFiles = ant.SearchRecursively("C:\directory\");
```