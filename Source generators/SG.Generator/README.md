# SimpleToJson 🚀

[![NuGet version](https://img.shields.io/nuget/v/SimpleToJson.svg)](https://www.nuget.org/packages/SimpleToJson/)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)

A lightweight and efficient NuGet package designed to easily inject simple, non-recursive `ToJson()` extension methods into your C# classes using source generation. Say goodbye to boilerplate code for basic JSON serialization!

## ✨ Features

* **Source Generated:** No runtime reflection overhead. The `ToJson()` method is generated at compile-time.
* **Simple Output:** Creates a basic JSON string `{ "Property1": value1, "Property2": value2, ... }` suitable for simple data models.
* **Easy to Use:** Just add the `[SimpleToJson]` attribute to your class.

## 📦 Installation

Install the package via the NuGet Package Manager console:

```bash
Install-Package SimpleToJson