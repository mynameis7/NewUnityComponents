namespace Core;

public abstract record EventBase {
    public string[] TargetTags {get; set;} = {};

}