# UdonTest

Simple test library for UdonSharp.

## Installation

To use this package, you need to add [my package repository](https://github.com/koyashiro/vpm-repos).
Please read more details [here](https://github.com/koyashiro/vpm-repos#installation).

Please install this package with [Creator Companion](https://vcc.docs.vrchat.com/) or [VPM CLI](https://vcc.docs.vrchat.com/vpm/cli/).

### Creator Companion

1. Enable the `koyashiro` package repository.

   ![image](https://user-images.githubusercontent.com/6698252/230629434-048cde39-a0ec-4c53-bfe2-46bde2e6a57a.png)

2. Find `UdonTest` from the list of packages and install any version you want.

### VPM CLI

1. Execute the following command to install the package.

   ```sh
   vpm add package net.koyashiro.udontest
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
