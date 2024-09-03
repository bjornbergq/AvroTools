# Avro Schema Generator

A tool to help generate CSharp classes from Avro schema files (avsc). 
This tool creates a single .cs file for each avsc, and not a folder structure (potential future feature)

## Usage
1. Create a test.avsc file. 
2. Add content in Avro schema format.
```json
{
	"type": "record",
	"name": "Test",
	"namespace": "se.bjornbergq",
	"doc": "The test type.",
	"fields": [
		{
			"name": "id",
			"type": "string",
			"doc": "The identifier, must not be null."
		},
		{
			"name": "name",
			"type": [
				"null",
				"string"
			],
			"doc": "The name, value can be null.",
			"default": null
		}
	]
}
```
3. Select properties on the file and type `AvroSchemaGenerator` in the field  `Custom Tool`
4. The geneartor will now generate CSharp file containing all types from the schema. If you want it to generate only one class from the file and use namespace references on others, use the generator called `AvroSchemaGeneratorSingle`
5. Done