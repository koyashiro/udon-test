# UdonTest

Simple test library for UdonSharp.

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

        Assert.False(true); // FAILED!
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

![image](https://user-images.githubusercontent.com/6698252/202898150-4e2b6421-ea0c-49d0-9e9e-6ae0f3e6f032.png)
