# microc

### 解释器 interpc

```sh
# 编译解释器 interpc 命令行程序
dotnet restore interpc.fsproj     # 可选
dotnet clean interpc.fsproj       # 可选
dotnet build -v n interpc.fsproj  # 构建 ./bin/Debug/net6.0/interpc，-v n 查看详细生成过程

# 执行解释器
./bin/Debug/net6.0/interpc example/ex_interpc.c 8
dotnet run --project interpc.fsproj example/ex_interpc.c 8
dotnet run --project interpc.fsproj -g example/ex_interpc.c 8  # 显示token AST 等调试信息

# one-liner
# 自行修改 interpc.fsproj  解释example目录下的源文件
#
# <MyItem Include="example/origin.c" Args ="8"/>
dotnet build -t:ccrun interpc.fsproj
```

### 优化编译器 microcc

```sh
dotnet restore microcc.fsproj
dotnet clean microcc.fsproj
dotnet build microcc.fsproj

# 两种方式执行
dotnet run --project microcc.fsproj example/ex_microcc.c  # 执行编译器（运行前需 clean）
./bin/Debug/net6.0/microcc example/ex_microcc.c           # 直接执行
```

### 虚拟机构建与运行

```sh
# dotnet
dotnet clean machine.csproj
dotnet run --project machine.csproj -t example/ex_machine.out 3  # 运行虚拟机（运行前需 clean），-t 查看跟踪信息
./bin/Debug/net6.0/machine -t example/ex_machine.out 3           # 直接执行

# c
gcc -o machine machine.c                   # 编译 c 虚拟机
./machine example/ex_machine.out 3         # 虚拟机执行指令
./machine -trace example/ex_machine.out 3  # -trace 查看跟踪信息
```