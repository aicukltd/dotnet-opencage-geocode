OpenCage Data Geocoding Library for .NET 6.0
=======================

A .NET 6.0 library that provides geocoding and reverse geocoding of locations using the  [OpenCage Geocoder](https://opencagedata.com/)
geocoder. 

## Dependencies and Requirements
* .NET 6.0
* ServiceStack.Text.Core ver 6.4 (added via Nuget)
* OpenCageGeocoder key - get yours [FREE](https://opencagedata.com/)

## Usage (Geocoding)
Reference the library using [Nuget](https://www.nuget.org/packages/OpenCage.Geocode.DotNetStandard/) 

Create an instance of the geocoder library, passing a valid [OpenCage Data Geocoder API key](https://opencagedata.com/) as a parameter to the geocoder library's constructor:

```C#
var geoCoder = new GeoCoder("YOUR_KEY");
```

Pass a string containing the query or address to be geocoded to the library's `Geocode` method:

```C#
var result = await geoCoder.GeoCodeAsync("82 Clerkenwell Road, London");
```

You will get a strongly typed GeocoderResponse object returned.

There are many parameters for language, country, bounds and more see https://opencagedata.com/api for explanations of them all or read the documentation provided for each parameter in Visual Studio.

Putting all of this together as a console app, a complete sample with a basic and advanced usage would look like this:


```C#
var geoCoder = new GeoCoder("YOURKEYHERE");

// simplest example with no optional parameters
var result = await geoCoder.GeoCodeAsync("newcastle");

//  example with lots of optional parameters
var result2 = await geoCoder.GeoCodeAsync("newcastle", countrycode: "gb", limit: 2, minConfidence: 6, language: "en", abbrv: true, noAnnotations:true, noRecord: true, addRequest: true);

```

## Usage (Reverse Geocoding)
Reverse geocoding is almost identical but you pass in a latitude and longitude pair:


```C#
var geoCoder = new Geocoder("YOUR_KEY");
var result = geoCoder.ReverseGeoCodeAsync(51.4277844, -0.3336517);
            
result.PrintDump(); // ServiceStack human readable object dump to console
```

There are many parameters for language, limiting results and more see https://opencagedata.com/api for explanations of them all or read the documentation provided for each parameter in Visual Studio.

## Further Examples
Further examples of a .Net Core and .Net 4.6.1 console application are available within the solution as projects 'GeocoderDemo.Net461' and 'GeocoderDemo.NetCore'.

## Error handling
Any errors that the geocoding service returns will be found in the **RequestStatus** property of the **GeocoderResponse** object. **RequestStatus** contains the standard HTTP error status code as the **Code** property and a more helpful error message in the **Message** property.

## Best practices
Before starting to use the OpenCage geocoder in your projects we advice you read the [Best Practices document](https://opencagedata.com/api#bestpractices) to ensure you get the best results possible.

## Rate limiting
Two error codes are used when any rate limiting has come into effect:

 1. 402 - 'Valid request but quota exceeded (payment required)'
 2. 429 - 'Too many requests (too quickly, rate limiting)'

For more about the rate limits read the [OpenCage Geocoder API Documentation](https://opencagedata.com/api#rate-limiting).

## Older versions for pre .Net 4.6.1
The previous release of this library was not targeted at .Net Standard but at .Net 4.6. The nuget package for this is still available at [https://www.nuget.org/packages/OpenCageGeocoder/](https://www.nuget.org/packages/OpenCageGeocoder/) but will not be maintained so we recommend using this new library and updating to a supported .Net version.
