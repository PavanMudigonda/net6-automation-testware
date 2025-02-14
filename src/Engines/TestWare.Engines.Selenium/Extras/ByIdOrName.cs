﻿
using System.Collections.ObjectModel;
using System.Globalization;
using OpenQA.Selenium;

namespace TestWare.Engines.Selenium.Extras;

/// <summary>
/// Finds element when the id or the name attribute has the specified value.
/// </summary>
public class ByIdOrName : By
{
    private readonly string elementIdentifier = string.Empty;
    private readonly By idFinder;
    private readonly By nameFinder;

    /// <summary>
    /// Initializes a new instance of the <see cref="ByIdOrName"/> class.
    /// </summary>
    /// <param name="elementIdentifier">The ID or Name to use in finding the element.</param>
    public ByIdOrName(string elementIdentifier)
    {
        if (string.IsNullOrEmpty(elementIdentifier))
        {
            throw new ArgumentException("element identifier cannot be null or the empty string", nameof(elementIdentifier));
        }

        this.elementIdentifier = elementIdentifier;
        this.idFinder = By.Id(this.elementIdentifier);
        this.nameFinder = By.Name(this.elementIdentifier);
    }

    /// <summary>
    /// Find a single element.
    /// </summary>
    /// <param name="context">Context used to find the element.</param>
    /// <returns>The element that matches</returns>
    public override IWebElement FindElement(ISearchContext context)
    {
        try
        {
            return this.idFinder.FindElement(context);
        }
        catch (NoSuchElementException)
        {
            return this.nameFinder.FindElement(context);
        }
    }

    /// <summary>
    /// Finds many elements
    /// </summary>
    /// <param name="context">Context used to find the element.</param>
    /// <returns>A readonly collection of elements that match.</returns>
    public override ReadOnlyCollection<IWebElement> FindElements(ISearchContext context)
    {
        List<IWebElement> elements = new List<IWebElement>();
        elements.AddRange(this.idFinder.FindElements(context));
        elements.AddRange(this.nameFinder.FindElements(context));

        return elements.AsReadOnly();
    }

    /// <summary>
    /// Writes out a description of this By object.
    /// </summary>
    /// <returns>Converts the value of this instance to a <see cref="string"/></returns>
    public override string ToString()
    {
        return string.Format(CultureInfo.InvariantCulture, "ByIdOrName([{0}])", this.elementIdentifier);
    }
}
