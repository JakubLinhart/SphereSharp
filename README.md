SphereSharp is experimental project to run SphereScript 0.99 under ServUO server. SphereServer 0.99 is a long dead project with no source code available. There are still some awesome shards running on more than a decade dead server.

It is not easy to migrate hundreds of thousands of lines that power theses shards to a newer [SphereServer](https://github.com/Sphereserver/Source) version because of script language incompatibility.

SphereServer 0.99 is far from a flawless software. It means that there are many random bugs and server crashes quite frequently which makes game play a little bit painful.

This is an experiment at a very early stage to make these shards a little bit better.

## Getting started

- Clone SphereSharp.
- Checkout ServUO submodule.
- Open SphereSharp.sln and start SphereSharp.ServUO.Generator.Console. This reads all *.scp scripts in TestScripts folder and translate all itemdef/chardef/gumps for ServUO. It writes resulting files to SphereSharp.ServUO.Generator.Console\Bin\Debug\output.
- Copy all files from SphereSharp.ServUO.Generator.Console\Bin\Debug\output to ServUO\Scripts\Sphere\Generated folder.
- Open SphereSharp.ServUO.sln.
- Include all files in ServUO\Scripts\Sphere\Generated to Scripts project.
- [Start](https://www.servuo.com/tutorials/double-your-coding-speed-shortcuts-tips-and-tricks-to-coding-faster.16/?page=1#section_38) Scripts project.

If you want to see what is already supported, take a look to TestScripts folder. It contains all compatible scripts from [Moria](https://github.com/SirGlorg/MoriaSphereScripts) shard.

## Goals

- Parse SphereScript 0.99 syntax as precise as possible to minimize changes required for script migration.

- Make runtime behavior as close as possible to SphereServer 0.99 to avoid game play changes.

- The final goal is to make script authoring a little bit easier (automated script testing, code completion, better source code analysis - errors, warnings).

I'm not sure how to achieve the final goal. I can imagine two possible successful results:

- SphereServer 0.99 reimplementation gradually replaces ServUO code, resulting into a new standalone emulator.

- SphereScript 0.99 runs on SphereServer 0.56 (or newer) - either it is possible to create a thin 0.99 <-> 0.56 adaptation layer or it is easy enough to implement 0.99 -> 0.56 transpiler.

## Why ServUO

A natural candidate for target platform is SphereServer 0.56 or newer version - it should be possible "just" to tweak the SphereServer 0.56 parser a little bit. Even at this early stage it is clear that ServUO has fundamentally different behavior and ServUO can't host SphereServer 0.99 scripts without major impact on game experience.

There are some reasons why C#/ServUO is still useful:

- For me C# is the best environment to learn SphereScript by re-implementing it :). ServUO provides a complete environment for testing work in progress.

- SphereServer source code is very old, without any test coverage. Making any changes seems to be very painful even more so when I have no experience with shard authoring. Reimplementation is much more fun :).

- Standalone SphereScript 0.99 parser would be useful even when SphereServer 0.56 replaces ServUO. This parser can be a foundation for transpiler (0.99 -> 0.56) or analyzers.