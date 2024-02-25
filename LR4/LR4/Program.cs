using LR4.Services;
using System;
using System.Reflection;

class Program {
    static void Main(String[] args) {
        Games valheim = new Games(1, "Valheim", 'A', 98.3, true);
        Games stickman = new Games(2, "Stickman", 'C', 2.6, false);

        valheim.GetFullInfo();
        stickman.GetGameId(stickman);


        // Using Type and TypeInfo
        Type valheimType = valheim.GetType();
        TypeInfo valheimTypeInfo = valheimType.GetTypeInfo();

        Console.WriteLine("\n====[ Type & TypeInfo ]====\n"
                          + "Valheim Type (BaseType): " + valheimType.BaseType
                          + "\nValheim TypeInfo (Namespace): " + valheimTypeInfo.Namespace) ;


        // Using MemberInfo
        Console.WriteLine("\n====[ MemberInfo ]====");
        foreach (MemberInfo member in valheimTypeInfo.GetMembers()) {
            Console.WriteLine(member.DeclaringType + " - " + member.Name + " - " + member.MemberType);
        }


        // Using FieldInfo
        Console.WriteLine("\n====[ FieldInfo ]====");
        foreach (FieldInfo fieldInfo in valheimTypeInfo.DeclaredFields) {
            Console.WriteLine(fieldInfo.Name + " - " + fieldInfo.FieldType);
        }


        // Using MethodInfo
        Console.WriteLine("\n====[ MethodInfo ]====");
        foreach (MethodInfo methodInfo in valheimTypeInfo.GetMethods()) {
            Console.WriteLine(methodInfo.Name + " - " + methodInfo.ReturnType + " - " + methodInfo.GetType());
        }


        // Method call with Reflection
        Console.Write("\n");

        MethodInfo valheimMethod = valheimType.GetMethod("GetFullInfo");

        try {
            if (valheimMethod != null) {
                valheimMethod.Invoke(valheim, null );
            }
        } catch (Exception e) {
            Console.WriteLine("Error: " + e.Message);
        }
    }
}