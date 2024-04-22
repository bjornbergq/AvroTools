using Avro;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell.Interop;
using System;
using System.Runtime.InteropServices;
using System.Text;

namespace VSIXAvroCustomTool
{
  /// <summary>
  /// When setting the 'Custom Tool' property of a C# project item to "AvroSchemaGenerator", 
  /// the GenerateCode function will get called and will return the contents of the generated file 
  /// to the project system
  /// </summary>
  public class AvroSchemaGenerator : IVsSingleFileGenerator
  {
    public int DefaultExtension(out string pbstrDefaultExtension)
    {
      pbstrDefaultExtension = ".cs";
      return VSConstants.S_OK;
    }

    public int Generate(string wszInputFilePath, string bstrInputFileContents, string wszDefaultNamespace, IntPtr[] rgbOutputFileContents, out uint pcbOutput, IVsGeneratorProgress pGenerateProgress)
    {
      pcbOutput = 0;

      // Generate code
      var gen = new CodeGen();
      var schema = Schema.Parse(bstrInputFileContents);
      gen.AddSchema(schema);

      gen.GenerateCode();
      var outputTypes = gen.GetTypes();

      if (!outputTypes.TryGetValue(schema.Name, out var outputType))
      {
        //pGenerateProgress.GeneratorError();
        return -2;
      }

      var outputBytes = Encoding.UTF8.GetBytes(outputType);
      pcbOutput = (uint)outputBytes.Length;
      rgbOutputFileContents[0] = Marshal.AllocCoTaskMem(outputBytes.Length);
      Marshal.Copy(outputBytes, 0, rgbOutputFileContents[0], outputBytes.Length);
      return VSConstants.S_OK;
    }
  }
}
