# Meta.NET
A .NET library for parsing metadata in web pages. This library has been ported to .NET from Mozilla's [Page Metadata Parser](https://github.com/mozilla/page-metadata-parser) for JavaScript.

[![Build status](https://ci.appveyor.com/api/projects/status/97r4xwk8v7h3o88w?svg=true)](https://ci.appveyor.com/project/chriszirkel/meta-net)

## Overview
### Installation
Available on NuGet: [Meta.NET](http://www.nuget.org/packages/Meta.NET/)
[![NuGet](https://img.shields.io/nuget/v/Meta.NET.svg)](https://www.nuget.org/packages/Meta.NET/)

The library is .NETStandard 1.4 with a dependency on [AngleSharp](https://www.nuget.org/packages/AngleSharp/) and [NETStandard.Library](https://www.nuget.org/packages/NETStandard.Library/).

### Purpose

The purpose of this library is to be able to find a consistent set of metadata for any given web page.  Each individual kind of metadata has many rules which define how it may be located.  For example, a description of a page could be found in any of the following DOM elements:

    <meta name="description" content="A page's description"/>

    <meta property="og:description" content="A page's description" />

Because different web pages represent their metadata in any number of possible DOM elements, the Metadata Parser collects rules for different ways a given kind of metadata may be represented and abstracts them away from the caller.

The output of the metadata parser for the above example would be a Dictionary with *description* as Key and *A page's description* as Value regardless of which particular kind of description tag was used.

    {description: "A page's description"}

### Supported schemas

This library employs parsers for the following formats:

[opengraph](http://ogp.me/)

[twitter](https://dev.twitter.com/cards/markup)

[meta tags](https://developer.mozilla.org/en/docs/Web/HTML/Element/meta)