﻿using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace JwtAuthSample.Models
{
    public class SecurityRequirementsDocumentFilter : IDocumentFilter
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="swaggerDoc"></param>
        /// <param name="context"></param>
        public void Apply(SwaggerDocument swaggerDoc, DocumentFilterContext context)
        {
            swaggerDoc.Security = new List<IDictionary<string, IEnumerable<string>>>()
            {
                new Dictionary<string, IEnumerable<string>>()
                {
                    { "Bearer", new string[]{ } },
                    { "Basic", new string[]{ } },
                }
            };

            foreach (ApiDescription apiDescription in context.ApiDescriptions)
            {
                if (apiDescription.TryGetMethodInfo(out MethodInfo method))
                {
                    if (method.ReflectedType.CustomAttributes.Any(t => t.AttributeType == typeof(HiddenApiAttribute))
                      || method.CustomAttributes.Any(t => t.AttributeType == typeof(HiddenApiAttribute)))
                    {
                        string key = "/" + apiDescription.RelativePath;
                        if (key.Contains("?"))
                        {
                            int idx = key.IndexOf("?", System.StringComparison.Ordinal);
                            key = key.Substring(0, idx);
                        }
                        swaggerDoc.Paths.Remove(key);
                    }
                }
            }
        }
    }
}
