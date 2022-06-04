#!/bin/bash
root_path="/Users/chenli/Documents/Course/GThree/compile/final/microc/"
bin_path=$root_path"bin/Debug/net6.0"
ex_name=${1%.*}  # 去后缀

# 切换目录
cd $root_path
dotnet clean interpc.fsproj
dotnet run --project interpc.fsproj -g $ex_name.c ${@:2} > cmd/interpc_g.txt
$bin_path/interpc $ex_name.c ${@:2} > cmd/interpc.txt

dotnet clean microcc.fsproj
dotnet run --project microcc.fsproj $ex_name.c

dotnet clean machine.csproj
dotnet run --project machine.csproj -t $ex_name.out ${@:2} > cmd/machine_t.txt
$bin_path/machine $ex_name.out ${@:2} > cmd/machine.txt