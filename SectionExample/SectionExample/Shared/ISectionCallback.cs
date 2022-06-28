namespace SectionExample.Shared;

using Microsoft.AspNetCore.Components;

public interface ISectionCallback
{
    void SetMenu(RenderFragment? value);
}
