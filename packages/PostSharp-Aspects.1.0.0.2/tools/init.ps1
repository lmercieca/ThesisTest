param($installPath, $toolsPath, $package)

# Don't support old NuGet versions as it's impractical to handle all their different sets of sematics
if ([NuGet.PackageManager].Assembly.GetName().Version -lt 1.5) 
{
	throw "PostSharp-Aspects requires NuGet (Package Manager Console) 1.5 or later"
} 