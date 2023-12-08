using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;
using System.Text;
using WebApiDemoG.Models;

namespace WebApiDemoG.Formatters
{
    public class TextCsvOutputFornatter : TextOutputFormatter
    {
        static int count = 1;
        public TextCsvOutputFornatter()
        {
            SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("text/TCvs"));
            SupportedEncodings.Add(Encoding.UTF8);
            SupportedEncodings.Add(Encoding.Unicode);
        }
        public override Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectedEncoding)
        {
            var response = context.HttpContext.Response;
            var stringBuilder = new StringBuilder();
            if (context.Object is IEnumerable<StudentModel> list)
            {
                foreach (var model in list)
                {
                    FormatTCVS(stringBuilder, model);
                }
            }
            else
            {
                var contact = context.Object as StudentModel;
                FormatTCVS(stringBuilder, contact);
            }
            return response.WriteAsync(stringBuilder.ToString());
        }

        private static void FormatTCVS(StringBuilder builder, StudentModel model)
        {
            builder.AppendLine($"BEGIN:CSV {count++}");
            builder.AppendLine($"ID:{model.Id}");
            builder.AppendLine($"Name:{model.Name}");
            builder.AppendLine($"Age:{model.Age}");
            builder.AppendLine($"SeriaNo:{model.SeriaNo}");
            builder.AppendLine($"Score:{model.Score}");
            builder.AppendLine($"END:{model.Name} CSV");
        }

        protected override bool CanWriteType(Type? type)
        {
            if (typeof(StudentModel).IsAssignableFrom(type) ||
                typeof(IEnumerable<StudentModel>).IsAssignableFrom(type))
            {
                return base.CanWriteType(type);
            }
            return false;
        }
    }
}
