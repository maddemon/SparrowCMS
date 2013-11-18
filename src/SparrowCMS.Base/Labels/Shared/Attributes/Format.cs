namespace SparrowCMS.Base.Labels.Shared.Attributes
{
    public class Format : FieldAttribute
    {
        public override string ConvertFieldValue(object fieldValue)
        {
            if (fieldValue == null)
                return null;

            return Value.Replace("$this", fieldValue.ToString());
        }
    }
}
