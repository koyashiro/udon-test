# UdonTest

Simple test library for UdonSharp.

## Installation

```sh
vpm add repo https://vpm.koyashiro.net/repos.json
```

## Example

```cs
using UdonSharp;
using Koyashiro.UdonTest;

public class UdonTestSample : UdonSharpBehaviour
{
    public void Start()
    {
        Assert.True(true); // OK!
        Assert.True(false); // FAILED!

        Assert.False(false); // OK!
        Assert.False(true); // FAILED!

        Assert.Null(null); // OK!
        Assert.Null(""); // FAILED!

        Assert.Equal(1, 1); // OK!
        Assert.Equal(1, 2); // FAILED!
        Assert.Equal(1, 1f); // FAILED!

        Assert.Equal("valid", "valid"); // OK!
        Assert.Equal("valid", "invalid"); // FAILED!

        Assert.Equal(new string[] { "first", "second" }, new string[] { "first", "second" }); // OK!
        Assert.Equal(new string[] { "first", "second", "third" }, new string[] { "first", "second" }); // FAILED!
    }
}
```

![image](https://user-images.githubusercontent.com/6698252/202899749-1b069abc-863a-4786-9fd5-2313c35aa58e.png)
