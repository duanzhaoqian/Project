using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;

namespace TestTypeBuilder
{
    class Program
    {
        static void Main(string[] args)
        {
            AssemblyName assemblyName = new AssemblyName("DynimacHouseModel");
            AssemblyBuilder assemblyBuilder = AppDomain.CurrentDomain.DefineDynamicAssembly(assemblyName,
                AssemblyBuilderAccess.RunAndSave);
            ModuleBuilder moduleBuilder = assemblyBuilder.DefineDynamicModule(assemblyName.Name, assemblyName + ".dll");
            TypeBuilder typeBuilder = moduleBuilder.DefineType("HouseModel", TypeAttributes.Public);
            //PropertyBuilder propertyBuilder = typeBuilder.DefineProperty("Name", PropertyAttributes.HasDefault,
            //    typeof(string), null);
            //PropertyBuilder propertyBuilder1 = typeBuilder.DefineProperty("Age", PropertyAttributes.HasDefault,
            //typeof(int), null);
            //ConstructorBuilder constructorBuilder = typeBuilder.DefineConstructor(MethodAttributes.Public,
            //    CallingConventions.Standard, Type.EmptyTypes);
            //ILGenerator ilGenerator=constructorBuilder.GetILGenerator();
            //ilGenerator.Emit(OpCodes.Ldarg_0);
            FieldBuilder fieldBuilder = typeBuilder.DefineField("Name", typeof(string), FieldAttributes.Public);
            FieldBuilder fieldBuilder1 = typeBuilder.DefineField("Age", typeof(string), FieldAttributes.Public);
            FieldBuilder _p1FieldBuilder = typeBuilder.DefineField("_p1", typeof(string), FieldAttributes.Private);
            //FieldBuilder _p2FieldBuilder = typeBuilder.DefineField("_p2", typeof(string), FieldAttributes.Private);
            PropertyBuilder p1 = typeBuilder.DefineProperty("P1", PropertyAttributes.HasDefault,typeof(string), null);
    //        PropertyBuilder p2 = typeBuilder.DefineProperty("P2", PropertyAttributes.HasDefault,
    //CallingConventions.Standard, typeof(string), Type.EmptyTypes);
            MethodAttributes getSetAttributes = MethodAttributes.Public | MethodAttributes.SpecialName |
                                                MethodAttributes.HideBySig;
            MethodBuilder p1get = typeBuilder.DefineMethod("get_P1", getSetAttributes, typeof(string), Type.EmptyTypes);

            ILGenerator ilP1get = p1get.GetILGenerator();
            ilP1get.Emit(OpCodes.Ldarg_0);
            ilP1get.Emit(OpCodes.Ldfld, _p1FieldBuilder);
            ilP1get.Emit(OpCodes.Ret);

            MethodBuilder p1set = typeBuilder.DefineMethod("set_P1", getSetAttributes, null, new[] { typeof(string) });
            ILGenerator ilP1Set = p1set.GetILGenerator();
            ilP1Set.Emit(OpCodes.Ldarg_0);
            ilP1Set.Emit(OpCodes.Ldarg_1);
            ilP1Set.Emit(OpCodes.Stfld, _p1FieldBuilder);
            ilP1Set.Emit(OpCodes.Ret);

            p1.SetSetMethod(p1set);
            p1.SetGetMethod(p1get);

            Type type = typeBuilder.CreateType();
            assemblyBuilder.Save(assemblyName.Name + ".dll");
            var obj = Assembly.Load(assemblyBuilder.GetName()).CreateInstance(type.FullName);
            FieldInfo[] fieldInfos = obj.GetType().GetFields();
            PropertyInfo[] propertyInfos = obj.GetType().GetProperties();
            foreach (PropertyInfo info in propertyInfos)
            {
                info.SetValue(obj,"property",null);
            }
            foreach (FieldInfo info in fieldInfos)
            {
                info.SetValue(obj, "aaa");
            }
            int i = 0;
        }
    }
}
