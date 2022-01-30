using System;
using System.Collections.Generic;

public interface ILoadScreenView : IView
{
    event Action BackClicked;
    void DisplayLoadSlots(List<SaveFile> saveFiles);
}
