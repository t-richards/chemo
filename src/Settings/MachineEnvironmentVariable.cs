namespace Chemo.Settings
{
    /// <summary>
    /// A system-wide environment variable that should hold a specific value.
    /// </summary>
    internal sealed class MachineEnvironmentVariable : ISetting
    {
        public string Name { get; }
        public string Value { get; }

        public MachineEnvironmentVariable(string name, string value)
        {
            Name = name;
            Value = value;
        }

        public bool IsApplied()
        {
            return string.Equals(Environment.GetEnvironmentVariable(Name, EnvironmentVariableTarget.Machine), Value, StringComparison.Ordinal);
        }

        public void Apply()
        {
            Environment.SetEnvironmentVariable(Name, Value, EnvironmentVariableTarget.Machine);
        }

        public override string ToString()
        {
            return $"Environment variable {Name} = {Value}";
        }
    }
}
