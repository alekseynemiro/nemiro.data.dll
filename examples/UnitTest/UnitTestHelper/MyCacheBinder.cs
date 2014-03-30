using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace UnitTestHelper
{
  public class MyCacheBinder : System.Runtime.Serialization.SerializationBinder
  {

    public override Type BindToType(string assemblyName, string typeName)
    {
      return Type.GetType(String.Format("{0}, {1}", typeName, Assembly.GetExecutingAssembly().FullName));
    }

  }
}
