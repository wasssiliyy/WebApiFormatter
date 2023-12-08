using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;
using System.Text;
using WebApiDemoG.Models;

namespace WebApiDemoG.Formatters
{
    public class TextCsvInputFormatter :TextInputFormatter
    {
        public TextCsvInputFormatter()
        {
            SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("text/csv"));
            SupportedEncodings.Add(Encoding.UTF8);
            SupportedEncodings.Add(Encoding.Unicode);
        }
        protected override bool CanReadType(Type? type)
        {
            return type == typeof(StudentModel);
        }

        public override async Task<InputFormatterResult> ReadRequestBodyAsync(InputFormatterContext context, Encoding encoding)
        {
            try
            {
                using (var reader = new StreamReader(context.HttpContext.Request.Body, encoding))
                {
                    var content = await reader.ReadToEndAsync();
                    var model = new StudentModel();

                    var values = content.Split('-').ToList();
                        if (values.Count==5) {
                            values.RemoveAt(0);
                            model = new StudentModel(values);
                        }
                        else if(values.Count == 4) {
                            model = new StudentModel(values);
                        }
                    return await InputFormatterResult.SuccessAsync(model);
                }
            }
            catch (Exception ex)
            {
                return await InputFormatterResult.FailureAsync();
            }
        }
    }
}
