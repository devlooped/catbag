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

## Sponsors

<h3 style="vertical-align: text-top" id="by-clarius">
<img src="https://raw.githubusercontent.com/devlooped/oss/main/assets/images/sponsors.svg" alt="sponsors" height="36" width="36" style="vertical-align: text-top; border: 0px; padding: 0px; margin: 0px">&nbsp;&nbsp;by&nbsp;<a href="https://github.com/clarius">@clarius</a>&nbsp;<img src="https://raw.githubusercontent.com/clarius/branding/main/logo/logo.svg" alt="sponsors" height="36" width="36" style="vertical-align: text-top; border: 0px; padding: 0px; margin: 0px">
</h3>

*[get mentioned here too](https://github.com/sponsors/devlooped)!*
