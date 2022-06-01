#!/bin/bash
root_path="/Users/chenli/Documents/Course/GThree/compile/final/microc/"
bin_path=$root_path"bin/Debug/net6.0"
ex_name=${1%.*}  # 去后缀

# 切换目录
cd $root_path
$bin_path/interpc $ex_name.c ${@:2} > cmd/interpc.txt
# 异常处理
if [ "$?" != "0" ]
then
    echo "ERROR: interpc cmd not found"
    dotnet clean interpc.fsproj
    dotnet run --project interpc.fsproj $ex_name.c ${@:2} > cmd/interpc.txt
fi
$bin_path/interpc -g $ex_name.c ${@:2} > cmd/interpc_g.txt
$bin_path/microcc $ex_name.c
if [ "$?" != "0" ]
then
    echo "ERROR: microcc cmd not found"
    dotnet clean microcc.fsproj
    dotnet run --project microcc.fsproj $ex_name.c
fi
$bin_path/machine $ex_name.out ${@:2} > cmd/machine.txt
if [ "$?" != "0" ]
then
    echo "ERROR: machine cmd not found"
    dotnet clean machine.csproj
    dotnet run --project machine.csproj $ex_name.out ${@:2} > cmd/machine.txt
fi
$bin_path/machine -t $ex_name.out ${@:2} > cmd/machine_t.txt