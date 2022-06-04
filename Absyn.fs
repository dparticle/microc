(* File MicroC/Absyn.fs
   Abstract syntax of micro-C, an imperative language.
   sestoft@itu.dk 2009-09-25

   Must precede Interp.fs, Comp.fs and Contcomp.fs in Solution Explorer
 *)

module Absyn

// 基本类型
// 注意，数组、指针是递归类型
// 这里没有函数类型，注意与上次课的 MicroML 对比
type typ =
  | TypI                             (* Type int                    *)
  | TypC                             (* Type char                   *)
  | TypA of typ * int option         (* Array type                  *)
  | TypP of typ                      (* Pointer type                *)
  | TypS

and expr =                           // 表达式，右值
  | Access of access                 (* x    or  *p    or  a[e]     *) //访问左值（右值）
  | Assign of access * expr          (* x=e  or  *p=e  or  a[e]=e   *)
  | Addr of access                   (* &x   or  &*p   or  &a[e]    *)
  | CstI of int                      (* Constant                    *)
  | CstS of string                   (* string                      *)
  | Prim1 of string * expr           (* Unary primitive operator    *)
  | Prim2 of string * expr * expr    (* Binary primitive operator   *)
  | Prim3 of expr * expr * expr
  | Prim4 of string * access * expr
  | Andalso of expr * expr           (* Sequential and              *)
  | Orelse of expr * expr            (* Sequential or               *)
  | Call of string * expr list       (* Function call f(...)        *)
  | PreSelf of string * access       (* ++i/--i                     *)
  | PostSelf of string * access      (* i++/i--                     *)
  | Maxin of string * expr * expr    (* max/min                     *)
  | Abs of expr                      (* abs                         *)

and access =                         //左值，存储的位置
  | AccVar of string                 (* Variable access        x    *)
  | AccDeref of expr                 (* Pointer dereferencing  *p   *)
  | AccIndex of access * expr        (* Array indexing         a[e] *)

and stmt =
  | If of expr * stmt * stmt         (* Conditional                 *)
  | While of expr * stmt             (* While loop                  *)
  | For of expr * expr * expr * stmt (* For loop                    *)
  | ForRange1 of access * expr * stmt (* for access in range(expr) stmt *)
  | ForRange2 of access * expr * expr * stmt (* for access in range(expr, expr) stmt *)
  | ForRange3 of access * expr * expr * expr * stmt (* for access in range(expr, expr) stmt *)
  | Expr of expr                     (* Expression statement   e;   *)
  | Return of expr option            (* Return from method          *)
  | Block of stmtordec list          (* Block: grouping and scope   *)
  | Switch of expr * stmts list       (* Switch                      *)
  // 语句块内部，可以是变量声明 或语句的列表

and stmts =
  | Case of expr * stmt              (* Switch case                 *)
  | Default of stmt                  (* Switch default              *)

and stmtordec =
  | Dec of typ * string              (* Local variable declaration  *)
  | DecAssign of typ * string * expr
  | Stmt of stmt                     (* A statement                 *)

// 顶级声明 可以是函数声明或变量声明
and topdec =
  | Fundec of typ option * string * (typ * string) list * stmt
  | Vardec of typ * string
  | VardecAssign of typ * string * expr

// 程序是顶级声明的列表
and program =
  | Prog of topdec list
