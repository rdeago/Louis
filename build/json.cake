// Copyright (C) Tenacom and contributors. Licensed under the MIT license.
// See LICENSE file in the project root for full license information.

#nullable enable

// ---------------------------------------------------------------------------------------------
// JSON helpers
// ---------------------------------------------------------------------------------------------

using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Nodes;

using SysFile = System.IO.File;

/*
 * Summary : Parses a JSON object from a string. Fails the build if not successful.
 * Params  : str         - The string to parse.
 *           description - A description of the string for exception messages.
 * Returns : The parsed object.
 */
static JsonObject ParseJsonObject(string str, string description = "The provided string")
{
    JsonNode? node;
    try
    {
        node = JsonNode.Parse(
            str,
            new JsonNodeOptions { PropertyNameCaseInsensitive = false },
            new JsonDocumentOptions
            {
                AllowTrailingCommas = true,
                CommentHandling = JsonCommentHandling.Skip,
            });
    }
    catch (JsonException)
    {
        Fail($"{description} is not valid JSON.");
        throw null;
    }

    return node switch {
        null => Fail<JsonObject>($"{description} was parsed as JSON null."),
        JsonObject obj => obj,
        object other => Fail<JsonObject>($"{description} was parsed as a {other.GetType().Name}, not a {nameof(JsonObject)}."),
    };
}

/*
 * Summary : Loads a JSON object from a file. Fails the build if not successful.
 * Params  : path - The path of the file to parse.
 * Returns : The parsed object.
 */
static JsonObject LoadJsonObject(FilePath path)
{
    var fullPath = path.FullPath;
    JsonNode? node;
    try
    {
        using var stream = SysFile.OpenRead(fullPath);
        node = JsonNode.Parse(
            stream,
            new JsonNodeOptions { PropertyNameCaseInsensitive = false },
            new JsonDocumentOptions
            {
                AllowTrailingCommas = true,
                CommentHandling = JsonCommentHandling.Skip,
            });
    }
    catch (IOException e)
    {
        Fail($"Could not read from {fullPath}: {e.Message}");
        throw null;
    }
    catch (JsonException)
    {
        Fail($"{fullPath} does not contain valid JSON.");
        throw null;
    }

    return node switch {
        null => Fail<JsonObject>($"{fullPath} was parsed as JSON null."),
        JsonObject obj => obj,
        object other => Fail<JsonObject>($"{fullPath} was parsed as a {other.GetType().Name}, not a {nameof(JsonObject)}."),
    };
}

/*
 * Summary : Saves a JSON object to a file. Fails the build if not successful.
 * Params  : path - The path of the file to parse.
 * Returns : The parsed object.
 */
static void SaveJson(JsonNode json, FilePath path)
{
    var fullPath = path.FullPath;
    try
    {
        using var stream = SysFile.OpenWrite(fullPath);
        var writerOptions = new JsonWriterOptions
        {
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            Indented = true,
        };

        using var writer = new Utf8JsonWriter(stream, writerOptions);
        json.WriteTo(writer);
        stream.SetLength(stream.Position);
    }
    catch (IOException e)
    {
        Fail($"Could not write to {fullPath}: {e.Message}");
        throw null;
    }
}

/*
 * Summary : Gets the value of a property from a JSON object. Fails the build if not successful.
 * Types   : T            - The desired type of the property value.
 * Params  : json         - The JSON object.
 *           propertyName - The name of the property to get.
 *           description  - A description of the object for exception messages.
 * Returns : The value of the specified property.
 */
static T GetJsonPropertyValue<T>(JsonObject json, string propertyName, string objectDescription = "JSON object")
{
    Ensure(json.TryGetPropertyValue(propertyName, out var property), $"Json property {propertyName} not found in {objectDescription}.");
    switch (property)
    {
        case null:
            return Fail<T>($"Json property {propertyName} in {objectDescription} is null.");
        case JsonValue value:
            Ensure(value.TryGetValue<T>(out var result), $"Json property {propertyName} in {objectDescription} cannot be converted to a {typeof(T).Name}.");
            return result ?? Fail<T>($"Json property {propertyName} in {objectDescription} has a null value.");
        default:
            return Fail<T>($"Json property {propertyName} in {objectDescription} is a {property.GetType().Name}, not a {nameof(JsonValue)}.");
    }
}
