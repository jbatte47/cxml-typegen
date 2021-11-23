# cxml-typegen

> Generate C# types for XML serialization/deserialization using the latest cXML schema (DTD-based).

This tool will:

1. download the latest official cXML DTD files if not provided
2. download the w3.org Perl `dtd2xsd.pl` script if not provided
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
