namespace SparrowCMS.Labels.Shared.Functions
{
    public class Format : FieldFunction
    {
        public override string ConvertFieldValue(object fieldValue)
        {
            if (fieldValue == null)
                return null;

            return Value.Replace("$this", fieldValue.ToString());
        }
    }
}
