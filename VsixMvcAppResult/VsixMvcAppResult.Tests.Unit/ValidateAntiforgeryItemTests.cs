using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Web.Mvc;
using VsixMvcAppResult.UI.Web;
using System.Text;

namespace VsixMvcAppResult.Tests.Unit
{
    [TestClass]
    public class ValidateAntiforgeryItemTests
    {
        [TestMethod]
        public void AllHttpPostMethodsHaveAntiforgeryItemAttribute()
        {
            var controllerTypes = typeof(MvcApplication)
                .Assembly
                .DefinedTypes
                .Where(t => typeof(Controller).IsAssignableFrom(t));

            var httpPostMethods = controllerTypes
                .SelectMany(m => m.DeclaredMethods
                                .Where(dm => dm.CustomAttributes
                                                .Any(ca => (ca.AttributeType == typeof(AcceptVerbsAttribute))
                                                        && (ca.ConstructorArguments.Any(e => (
                                                            ((HttpVerbs)e.Value).HasFlag(HttpVerbs.Post)
                                                            )
                                                        )))
                                            ||
                                            dm.CustomAttributes.Any(ca => ca.AttributeType == typeof(HttpPostAttribute))
                    ));

            var validMethods = httpPostMethods
                .Where(x => x.CustomAttributes
                            .Any(ca => ca.AttributeType == typeof(ValidateAntiForgeryTokenAttribute)));

            var invalidMethods = httpPostMethods.Except(validMethods);

            var grouped = invalidMethods.GroupBy(x => x.DeclaringType.FullName);

            StringBuilder sb = new StringBuilder();
            foreach (var item in grouped)
            {
                sb.Append(string.Format("\n\n {0} \n\n", item.Key));
                sb.Append(string.Join("\t\t\n", item.Select(x => x.ToString())));
            }

            Assert.AreEqual(0, invalidMethods.Count(), sb.ToString());
                    
        }

        

    }
}
