namespace SparrowCMS.Labels.Shared.Functions
{
    public class Format : FieldFunction
    {
        public override string GetValue(object fieldValue)
        {
            if (fieldValue == null)
                return null;

            return RawValue.Replace("$this", fieldValue.ToString());
        }
    }
}
