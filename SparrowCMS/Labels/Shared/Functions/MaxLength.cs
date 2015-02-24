namespace SparrowCMS.Labels.Shared.Functions
{
    public class MaxLength : FieldFunction
    {
        private int Length { get { return int.Parse(RawValue); } }

        public override string GetValue(object fieldValue)
        {
            var result = base.GetValue(fieldValue);

            var maxLength = 0;
            if (int.TryParse(RawValue, out maxLength) && result.Length > maxLength)
            {
                return result.Substring(0, Length) + "...";
            }

            return result;
        }
    }
}
