#!/bin/bash
root_path="<root_path>"
bin_path=$root_path"bin/Debug/net6.0"
ex_name=${1%.*}  # 去后缀

# 切换目录
cd $root_path
$bin_path/interpc $ex_name.c ${@:2} > cmd/interpc.txt
# 异常处理
if [ "$?" != "0" ]
then
    dotnet run --project interpc.fsproj $ex_name.c ${@:2} > cmd/interpc.txt
fi
$bin_path/interpc -g $ex_name.c ${@:2} > cmd/interpc_g.txt
$bin_path/microcc $ex_name.c > cmd/microcc.txt
if [ "$?" != "0" ]
then
    dotnet run --project microcc.fsproj $ex_name.c > cmd/microcc.txt
fi
$bin_path/machine $ex_name.out ${@:2} > cmd/machine.txt
if [ "$?" != "0" ]
then
    dotnet run --project machine.csproj $ex_name.out ${@:2} > cmd/machine.txt
fi
$bin_path/machine -t $ex_name.out ${@:2} > cmd/machine_t.txt