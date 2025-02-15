# :cat:bag

A repository of loose helpers, base clases and assorted code that can be trivially referenced 
individually or by folders, using [dotnet-file](https://github.com/devlooped/dotnet-file).

> **cat**:  
> 1. [cat](http://www.linfo.org/cat.html) is one of the most frequently used commands on Unix-like operating systems. 
> It has three related functions with regards to text files: displaying them, combining copies of them and creating new ones.
> 2. (bag): in spanish, "bolsa de gatos". In Argentina, roughly a "mixed bag" of code, especially 
> when referring to projects like "Helpers" and "Common" which everyone hates but can't quite avoid.

You can efectively "cat" files from this repo straight into yours. 

Each piece of functionality that extends the dotnet framework (or any library, actually) in this manner, 
is hereafter known as a 🐱 (`:cat:`). 


For example, running the following on your repository root:

```
> dotnet file add https://github.com/devlooped/catbag/blob/main/System/Xml/XmlWrappingReader.cs src/Common/
```

Will download and add the file to `src/Common/System/Xml/XmlWrappingReader.cs` (you `cat` the remote file). 
If no target directory is specified, the file will be downloaded to the current directory, under `System/Xml`. 
If you want to downlodad it without a directory structure at all, to the current directory, you can pass `.` 
as the target directory.

Once downloaded, [dotnet-file](https://github.com/devlooped/dotnet-file) creates (or updates) a 
[dotnet-config](https://dotnetconfig.org/) file in the current directory named `.netconfig` as follows:

```
[file "src/Common/System/Xml/XmlWrappingReader.cs"]
	url = https://github.com/devlooped/catbag/blob/main/System/Xml/XmlWrappingReader.cs
	sha = 19be5e474022ab5b8993b29509a3929439f510e9
	etag = a4dba639f18b47a9e480704abf86b1ba2582c54b76a520d7ed988aa1efcd7b8d
```

The `url` is used in combination with the `etag` to detect changes in the source file afterwards, whenever you 
run `dotnet file update`. The `sha` is used if you pass a `-c|--changelog` file path to the command, which will 
contain the diff between the local `sha` and the latest one at the source. See [PR#46](https://github.com/dotnetconfig/dotnet-config/pull/46) and [PR#40](https://github.com/devlooped/dotnet-file/pull/40) for example.

You can easily automate running these checks for updates in CI on a schedule, to automatically generate those 
update PRs shown above. See [dotnet-file.yml](https://github.com/devlooped/oss/blob/main/.github/workflows/dotnet-file.yml) as an example 
that does this using GitHub Actions.

<!-- include https://github.com/devlooped/sponsors/raw/main/footer.md -->
# Sponsors 

<!-- sponsors.md -->
[![Clarius Org](https://raw.githubusercontent.com/devlooped/sponsors/main/.github/avatars/clarius.png "Clarius Org")](https://github.com/clarius)
[![MFB Technologies, Inc.](https://raw.githubusercontent.com/devlooped/sponsors/main/.github/avatars/MFB-Technologies-Inc.png "MFB Technologies, Inc.")](https://github.com/MFB-Technologies-Inc)
[![Torutek](https://raw.githubusercontent.com/devlooped/sponsors/main/.github/avatars/torutek-gh.png "Torutek")](https://github.com/torutek-gh)
[![DRIVE.NET, Inc.](https://raw.githubusercontent.com/devlooped/sponsors/main/.github/avatars/drivenet.png "DRIVE.NET, Inc.")](https://github.com/drivenet)
[![Keith Pickford](https://raw.githubusercontent.com/devlooped/sponsors/main/.github/avatars/Keflon.png "Keith Pickford")](https://github.com/Keflon)
[![Thomas Bolon](https://raw.githubusercontent.com/devlooped/sponsors/main/.github/avatars/tbolon.png "Thomas Bolon")](https://github.com/tbolon)
[![Kori Francis](https://raw.githubusercontent.com/devlooped/sponsors/main/.github/avatars/kfrancis.png "Kori Francis")](https://github.com/kfrancis)
[![Toni Wenzel](https://raw.githubusercontent.com/devlooped/sponsors/main/.github/avatars/twenzel.png "Toni Wenzel")](https://github.com/twenzel)
[![Uno Platform](https://raw.githubusercontent.com/devlooped/sponsors/main/.github/avatars/unoplatform.png "Uno Platform")](https://github.com/unoplatform)
[![Dan Siegel](https://raw.githubusercontent.com/devlooped/sponsors/main/.github/avatars/dansiegel.png "Dan Siegel")](https://github.com/dansiegel)
[![Reuben Swartz](https://raw.githubusercontent.com/devlooped/sponsors/main/.github/avatars/rbnswartz.png "Reuben Swartz")](https://github.com/rbnswartz)
[![Jacob Foshee](https://raw.githubusercontent.com/devlooped/sponsors/main/.github/avatars/jfoshee.png "Jacob Foshee")](https://github.com/jfoshee)
[![](https://raw.githubusercontent.com/devlooped/sponsors/main/.github/avatars/Mrxx99.png "")](https://github.com/Mrxx99)
[![Eric Johnson](https://raw.githubusercontent.com/devlooped/sponsors/main/.github/avatars/eajhnsn1.png "Eric Johnson")](https://github.com/eajhnsn1)
[![Ix Technologies B.V.](https://raw.githubusercontent.com/devlooped/sponsors/main/.github/avatars/IxTechnologies.png "Ix Technologies B.V.")](https://github.com/IxTechnologies)
[![David JENNI](https://raw.githubusercontent.com/devlooped/sponsors/main/.github/avatars/davidjenni.png "David JENNI")](https://github.com/davidjenni)
[![Jonathan ](https://raw.githubusercontent.com/devlooped/sponsors/main/.github/avatars/Jonathan-Hickey.png "Jonathan ")](https://github.com/Jonathan-Hickey)
[![Charley Wu](https://raw.githubusercontent.com/devlooped/sponsors/main/.github/avatars/akunzai.png "Charley Wu")](https://github.com/akunzai)
[![Jakob Tikjøb Andersen](https://raw.githubusercontent.com/devlooped/sponsors/main/.github/avatars/jakobt.png "Jakob Tikjøb Andersen")](https://github.com/jakobt)
[![Tino Hager](https://raw.githubusercontent.com/devlooped/sponsors/main/.github/avatars/tinohager.png "Tino Hager")](https://github.com/tinohager)
[![Ken Bonny](https://raw.githubusercontent.com/devlooped/sponsors/main/.github/avatars/KenBonny.png "Ken Bonny")](https://github.com/KenBonny)
[![Simon Cropp](https://raw.githubusercontent.com/devlooped/sponsors/main/.github/avatars/SimonCropp.png "Simon Cropp")](https://github.com/SimonCropp)
[![agileworks-eu](https://raw.githubusercontent.com/devlooped/sponsors/main/.github/avatars/agileworks-eu.png "agileworks-eu")](https://github.com/agileworks-eu)
[![sorahex](https://raw.githubusercontent.com/devlooped/sponsors/main/.github/avatars/sorahex.png "sorahex")](https://github.com/sorahex)
[![Zheyu Shen](https://raw.githubusercontent.com/devlooped/sponsors/main/.github/avatars/arsdragonfly.png "Zheyu Shen")](https://github.com/arsdragonfly)
[![Vezel](https://raw.githubusercontent.com/devlooped/sponsors/main/.github/avatars/vezel-dev.png "Vezel")](https://github.com/vezel-dev)
[![ChilliCream](https://raw.githubusercontent.com/devlooped/sponsors/main/.github/avatars/ChilliCream.png "ChilliCream")](https://github.com/ChilliCream)
[![4OTC](https://raw.githubusercontent.com/devlooped/sponsors/main/.github/avatars/4OTC.png "4OTC")](https://github.com/4OTC)
[![Vincent Limo](https://raw.githubusercontent.com/devlooped/sponsors/main/.github/avatars/v-limo.png "Vincent Limo")](https://github.com/v-limo)
[![Jordan S. Jones](https://raw.githubusercontent.com/devlooped/sponsors/main/.github/avatars/jordansjones.png "Jordan S. Jones")](https://github.com/jordansjones)
[![domischell](https://raw.githubusercontent.com/devlooped/sponsors/main/.github/avatars/DominicSchell.png "domischell")](https://github.com/DominicSchell)
[![Joseph Kingry](https://raw.githubusercontent.com/devlooped/sponsors/main/.github/avatars/jkingry.png "Joseph Kingry")](https://github.com/jkingry)


<!-- sponsors.md -->

[![Sponsor this project](https://raw.githubusercontent.com/devlooped/sponsors/main/sponsor.png "Sponsor this project")](https://github.com/sponsors/devlooped)
&nbsp;

[Learn more about GitHub Sponsors](https://github.com/sponsors)

<!-- https://github.com/devlooped/sponsors/raw/main/footer.md -->
