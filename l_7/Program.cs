using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using AnimalLibrary;

public class Program
{
    public static void SerializeToXml<T>(T target, XmlWriter writer)
    {
        writer.WriteStartElement($"{target.GetType().Name}");
        writer.WriteStartElement("Class_Fields");
        FieldInfo[] fields = target.GetType().GetFields();
        for (int i = 0; i < fields.Length; ++i) { writer.WriteElementString("Class_Field", fields[i].Name); }
        writer.WriteEndElement();
        writer.WriteStartElement("Methods");
        foreach (MemberInfo member in target.GetType().GetMembers())
        {
            writer.WriteElementString($"{member.DeclaringType.ToString().Replace(".", "_")}_{member.MemberType.ToString().Replace(".", "_")}", $"{member.Name.ToString().Replace(".", "_")}");
        }
        writer.WriteEndElement();
        writer.WriteElementString("BaseType", target.GetType().BaseType.Name);
        CommentAtt res = (CommentAtt)Attribute.GetCustomAttribute(target.GetType(), typeof(CommentAtt));
        if (res != null) { writer.WriteElementString(res.GetType().Name, res.Comment); }
        writer.WriteEndElement();
    }
    public static void SerializeEnumToXml<T>(XmlWriter writer)
    {
        writer.WriteStartElement($"{typeof(T).Name}_values");
        System.Type enumType = typeof(T);
        System.Type enumUnderlyingType = System.Enum.GetUnderlyingType(enumType);
        System.Array enumValues = System.Enum.GetValues(enumType);
        for (int i = 0; i < enumValues.Length; i++) { writer.WriteElementString("value", enumValues.GetValue(i).ToString()); }
        writer.WriteEndElement();
    }
    static void Main(string[] args)
    {
        Animal animal_test = new Animal("any", true, "AnimalName", AnimalClassification.Omnivores);
        Cow cow_test = new Cow("Vatican", true, "CowName", AnimalClassification.Herbivores);
        Lion lion_test = new Lion("Russia", false, "LionName", AnimalClassification.Carnivores);
        Pig pig_test = new Pig("Panama", true, "PigName", AnimalClassification.Omnivores);
        try
        {
            using XmlWriter writer = XmlWriter.Create($"result.xml");
            writer.WriteStartElement("Library");
            writer.WriteStartElement("Classes");
            SerializeToXml<Animal>(animal_test, writer);
            SerializeToXml<Cow>(cow_test, writer);
            SerializeToXml<Lion>(lion_test, writer);
            SerializeToXml<Pig>(pig_test, writer);
            writer.WriteEndElement();
            SerializeEnumToXml<AnimalClassification>(writer);
            SerializeEnumToXml<FavouriteFood>(writer);
            writer.WriteEndElement();
            writer.Flush();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error creating xml file: {ex.Message}");
        }
    }
}
