@ECHO OFF
SET root_path=%~dp0/../src/SphereSharp/

docker run --rm -v %root_path%:/work spheresharp/antlr:4.7 /work/sphereScript99.g4 -o /work/Parser -Dlanguage=CSharp -visitor