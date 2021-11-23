# cXML.TypeGen

> Generate C# types for XML serialization/deserialization using the latest cXML schema (DTD-based).


## Background

cXML stands for `commerce eXtensible Markup Language`, and represents a somewhat ubiquitous standard for business-to-business ([B2B](https://en.wikipedia.org/wiki/Business-to-business)) communication. This project's goal is to create reliably serializable/deserializable C# code that accurately represents the request/response bodies of cXML message exchanges. Due to the dynamic nature of message content in production deployments, part of the project's goal is to supplement the standardized structure the project outputs with an API that enables seamless data exchange while minimizing specialized error handling logic. Thus, `cXML.TypeGen` will provide a CLI mechanism for generating a base layer of type definitions based on the latest changes from the official cXML document source, as well as an extension layer that provides ease-of-use mechanisms for working with cXML in various use cases. Read more about cXML [here](http://cxml.org/) and [here](https://en.wikipedia.org/wiki/CXML).

This tool will:

1. download the [latest official cXML DTD files](http://cxml.org/downloads.html) if not provided
2. download the [w3.org `dtd2xsd.pl` Perl script](https://www.w3.org/2000/04/schema_hack/) if not provided
3. convert the DTD to XSD format
4. use the `XmlSchemaClassGenerator` package to generate C# assets

That's it!


### Installation

```sh
dotnet tool install --global cXML.TypeGen
```


### Usage

```sh
cxml-typegen --help
```

:point_up: this produces something similar to:

```txt
  -n, --namespace    (Default: Api.cXML) The namespace to generate C# classes for.

  -f, --filename     (Default: Model.cs) The name of the output file to generate.

  -o, --output       (Default: ./) The path to write output to.

  -d, --dtd          The path to the cXML DTD file to use. If not provided, will be downloaded
                     from the source. More info: http://cxml.org/downloads.html

  -c, --converter    The path to the DTD-to-XSD converter to use. If not provided, will be
                     downloaded from the source. More info: https://www.w3.org/2000/04/schema_hack/

  --help             Display this help screen.

  --version          Display version information.
```

### Dependencies

#### Environment

 - [.NET 5+](https://dotnet.microsoft.com/download)
 - [Perl](https://www.perl.org/)

#### Runtime (Nuget)
 - [Autofac 6.3](https://www.nuget.org/packages/Autofac/6.3.0)
 - [CommandLineParser 2.8](https://www.nuget.org/packages/CommandLineParser/2.8.0)
 - [XmlSchemaClassGenerator-beta 2](https://www.nuget.org/packages/XmlSchemaClassGenerator-beta/2.0.594)
