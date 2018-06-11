﻿using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SphereSharp.Sphere99;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SphereSharp.Tests.Sphere99.Sphere56Transpiler
{
    [TestClass]
    public class CallFunctionTests
    {
        [TestMethod]
        [DataRow("fun1", "fun1")]
        [DataRow("<argv(1)>_<arg(u)>", "<argv[1]>_<local.u>")]
        [DataRow("fun1()", "fun1")]
        [DataRow("fun1(-1)", "fun1 -1")]
        [DataRow("fun1(0abc)", "fun1 0abc")]
        [DataRow("fun1(#0abc)", "fun1 0abc")]
        [DataRow("fun1_2", "fun1_2")]
        [DataRow("fun1_2(3)", "fun1_2 3")]
        [DataRow("fun1_2(3,4,5)", "fun1_2 3,4,5")]
        [DataRow("a.b.c.fun1", "a.b.c.fun1")]
        [DataRow("a.b.c.fun1(1,2,3)", "a.b.c.fun1 1,2,3")]
        [DataRow("sysmessage(Zameruj jen monstra)", "sysmessage Zameruj jen monstra")]
        [DataRow("fun1(<eval fun2>)", "fun1 <eval <fun2>>")]
        [DataRow("fun1(<eval fun2(1,2,3)>)", "fun1 <eval <fun2 1,2,3>>")]
        [DataRow("fun1(<eval fun2(<eval 1>,<eval 2>,<eval 3>)>)", "fun1 <eval <fun2 <eval 1>,<eval 2>,<eval 3>>>")]
        [DataRow("fun1(<a1><a2><a3>)", "fun1 <a1><a2><a3>")]
        [DataRow("fun1(<a1><a2><a3>,<a1><a2><a3>)", "fun1 <a1><a2><a3>,<a1><a2><a3>")]
        [DataRow("fun1(<?a1?><?a2?><?a3?>)", "fun1 <a1><a2><a3>")]
        [DataRow("fun1(<?a1?><?a2?><?a3?>,<?a1?><?a2?><?a3?>)", "fun1 <a1><a2><a3>,<a1><a2><a3>")]
        public void Can_transpile_custom_function_calls(string src, string expectedResult)
        {
            TranspileStatementCheck(src, expectedResult);
        }

        [TestMethod]
        [DataRow("args.color", "<args>.color")]
        [DataRow("<args>.color", "<args>.color")]
        [DataRow("arg(x,<eval argv>)", "local.x=<eval <args>>")]
        public void Arguments(string src, string expectedResult)
        {
            TranspileStatementCheck(src, expectedResult);
        }

        [TestMethod]
        [DataRow("sysmessage(\"\")", "sysmessage \"\"")]
        [DataRow("sysmessage(\"<fun1>\")", "sysmessage \"<fun1>\"")]
        [DataRow("sysmessage(\"<fun1(1,2,3)>\")", "sysmessage \"<fun1 1,2,3>\"")]
        [DataRow("sysmessage(\"<?fun1(1,2,3)?>\")", "sysmessage \"<fun1 1,2,3>\"")]
        [DataRow("sysmessage(\"<arg(x)>\")", "sysmessage \"<local.x>\"")]
        public void QuotedArguments(string src, string expectedResult)
        {
            TranspileStatementCheck(src, expectedResult);
        }

        [TestMethod]
        public void UnquotedArguments()
        {
            TranspileStatementCheck("sysmessage Some text <fun1>", "sysmessage Some text <fun1>");
            TranspileStatementCheck("sysmessage Some text <fun1(1,2,3)>", "sysmessage Some text <fun1 1,2,3>");
            TranspileStatementCheck("sysmessage Some text <?fun1(1,2,3)?>", "sysmessage Some text <fun1 1,2,3>");
            TranspileStatementCheck("sysmessage Some text <arg(x)>", "sysmessage Some text <local.x>");
            TranspileStatementCheck("arg(x,Some text <arg(x)>)", "local.x=Some text <local.x>");
            TranspileStatementCheck("arg(x,Some text <?arg(x)?>)", "local.x=Some text <local.x>");
            TranspileStatementCheck(
                "serv.allclients(sendpacket(0c0 <hval(<argv(0)>)> <split4bytes(<xxx>)> <split2bytes(<nid(<argv(1)>)>)>))",
                "serv.allclients sendpacket 0c0 <hval(<argv[0]>)> <split4bytes <xxx>> <split2bytes <nid <argv[1]>>>");

            TranspileFileCheck(@"[function fun1]
arg(sometext,1)
fun2(this is just sometext nothing more! no sometext variable replacement!)",
@"[function fun1]
local.sometext=1
fun2 this is just sometext nothing more! no sometext variable replacement!");
        }

        [TestMethod]
        [DataRow("lastnew.bounce", "new.bounce")]
        [DataRow("equip <lastnew>", "equip <new>")]
        [DataRow("equip lastnew", "equip <new>")]
        [DataRow("lastnew.timer=300", "new.timer=300")]
        [DataRow("arg(u,<Skill_Enticement.effect>)", "local.u=<serv.skill.enticement.effect>")]
        [DataRow("region.flag_underground", "region.underground")]
        [DataRow("profession=class_<arg(class)>", "skillclass=class_<local.class>")]
        [DataRow("return <profession>", "return <skillclass>")]
        public void Name_transformation(string src, string expectedResult)
        {
            TranspileStatementCheck(src, expectedResult);
        }

        [TestMethod]
        [DataRow("morex=0", "morex=0")]
        public void Assignment(string src, string expectedResult)
        {
            TranspileStatementCheck(src, expectedResult);
        }

        [TestMethod]
        [DataRow("<fun1>", "<fun1>")]
        [DataRow("(<fun1>)", "(<fun1>)")]
        [DataRow("fun1", "<fun1>")]
        [DataRow("(fun1)", "(<fun1>)")]
        [DataRow("(fun1==fun2)", "(<fun1>==<fun2>)")]
        [DataRow("{1 2}", "{1 2}")]
        [DataRow("<eval 123>", "<eval 123>")]
        [DataRow("tag(u)", "<tag0.u>")]
        [DataRow("var(u)", "<var0.u>")]
        [DataRow("<findid(i_item)>", "<findid.i_item>")]
        [DataRow("arg(length)", "<local.length>")]
        [DataRow("src.action == 1", "<src.action> == 1")]
        public void Conditions(string src, string expectedResult)
        {
            TranspileConditionCheck(src, expectedResult);
        }

        [TestMethod]
        [DataRow("4 >> 8", "4 <op_shiftright> 8")]
        public void Binary_shift_operators(string src, string expectedResult)
        {
            TranspileConditionCheck(src, expectedResult);
        }

        [TestMethod]
        public void Safe()
        {
            TranspileStatementCheck("safe(findid.i_something.remove)", "findid.i_something.remove");
            TranspileStatementCheck("safe findid.i_something.remove", "findid.i_something.remove");
            TranspileStatementCheck("safe(cast 020)", "cast 020");
            TranspileStatementCheck("safe(events=+e_something)", "events=+e_something");
            TranspileConditionCheck("safe fun1(<argv(0)>).isChar", "<fun1.<argv[0]>.isChar>");
            TranspileConditionCheck("safe(fun1(<argv(0)>).isChar)", "<fun1.<argv[0]>.isChar>");
            TranspileConditionCheck("safe.tag(orig_stealth)", "<tag0.orig_stealth>");
            TranspileConditionCheck("<safe.tag(orig_stealth)>", "<tag0.orig_stealth>");
            TranspileConditionCheck("safe(<eval (<args>)>)", "<eval (<args>)>");
        }

        [TestMethod]
        public void While_statement()
        {
            TranspileStatementCheck(@"while 1
    call1
endwhile",

@"while 1
    call1
endwhile");

            TranspileStatementCheck(@"while 1
endwhile",

@"while 1
endwhile");

            TranspileStatementCheck(@"while 1
    while 2
    endwhile
endwhile",

@"while 1
    while 2
    endwhile
endwhile");
        }

        [TestMethod]
        public void If_statement()
        {
            TranspileStatementCheck(@"if 1
    call1
endif",
@"if 1
    call1
endif");

            TranspileStatementCheck(@"if 1
    if 2
        call1
    endif
endif",
@"if 1
    if 2
        call1
    endif
endif");

            TranspileStatementCheck(@"if 1
    call1
else
    call2
endif",
@"if 1
    call1
else
    call2
endif");

            TranspileStatementCheck(@"if 1
    call1
elseif 2
    call2
else
    call3
endif",
@"if 1
    call1
elseif 2
    call2
else
    call3
endif");
            TranspileStatementCheck(@"if 1
    call1
elseif 2
    call2
elseif 3
    call3
else
    call4
endif",
@"if 1
    call1
elseif 2
    call2
elseif 3
    call3
else
    call4
endif");

            TranspileStatementCheck(@"if(0)
    call1
endif",
@"if (0)
    call1
endif");
        }

        [TestMethod]
        public void DoSwitch_statement()
        {
            TranspileStatementCheck(@"doswitch arg(x)
    call1
    call2
    call3
enddo",
@"doswitch <local.x>
    call1
    call2
    call3
enddo");

            TranspileStatementCheck(@"doswitch <arg(x)>
    call1
    call2
    call3
enddo",
@"doswitch <local.x>
    call1
    call2
    call3
enddo");
        }

        [TestMethod]
        [DataRow("return 1", "return 1")]
        [DataRow("return<x>", "return <x>")]
        [DataRow("return <x>", "return <x>")]
        [DataRow("trigger timer", "trigger @timer")]
        [DataRow("trigger @timer", "trigger @timer")]
        [DataRow("trigger(@timer)", "trigger @timer")]
        [DataRow("trigger(timer)", "trigger @timer")]
        [DataRow("go 4764,1362,10", "go 4764,1362,10")]
        [DataRow("findid(i_rune_discordance).remove", "findid.i_rune_discordance.remove")]
        [DataRow("findid(i_rune_discordance)", "findid.i_rune_discordance")]
        [DataRow("isevent(e_something)", "isevent.e_something")]
        [DataRow("isevent e_something", "isevent.e_something")]
        [DataRow("isevent.e_something", "isevent.e_something")]
        public void Native_functions(string source, string expectedResult)
        {
            TranspileStatementCheck(source, expectedResult);
        }

        [TestMethod]
        [DataRow("arg(length,<strlen(<argv(1)>)>+45)", "local.length=<eval strlen(<argv[1]>)>+45")]
        [DataRow("arg(length,strlen(<argv(1)>)+45)", "local.length=<eval strlen(<argv[1]>)>+45")]
        [DataRow("arg(length,<eval strlen(<argv(1)>)>+45)", "local.length=<eval strlen(<argv[1]>)>+45")]
        [DataRow("arg(u,<eval strcmpi(<argv(0)>,<argv(1)>)>)", "local.u=<eval strcmpi(<argv[0]>,<argv[1]>)>")]
        [DataRow("arg(u,strcmpi(<secondnumb>,skill_inscription))", "local.u=<eval strcmpi(<secondnumb>,skill_inscription)>")]
        [DataRow("arg(u,strcmpi(<secondnumb>,<skill_inscription>))", "local.u=<eval strcmpi(<secondnumb>,<serv.skill.inscription>)>")]
        [DataRow("tag(class,<strmid(\"class_necro\",6,20)>)", "tag.class=<strsub 6 20 class_necro>")]
        [DataRow("tag(class,<strmid(<x>,6,20)>)", "tag.class=<strsub 6 20 <x>>")]
        [DataRow(
            "arg(u,<eval strcmpi(<argv(0)>,someunquotedliterallookinglikefunctionname)>)",
            "local.u=<eval strcmpi(<argv[0]>,someunquotedliterallookinglikefunctionname)>")]
        public void Special_functions(string source, string expectedResult)
        {
            TranspileStatementCheck(source, expectedResult);
        }

        [TestMethod]
        [DataRow("strmatch(<args>,<arg(pattern)>)", "strmatch(<local.pattern>,<args>)")]
        public void Special_functions_in_condition(string source, string expectedResult)
        {
            TranspileConditionCheck(source, expectedResult);
        }

        [TestMethod]
        public void Local_variables()
        {
            TranspileStatementCheck("arg(u,1)", "local.u=1");
            TranspileStatementCheck("arg(u,arg(v))", "local.u=<local.v>");
            TranspileStatementCheck("arg(u,<argcount>)", "local.u=<argv>");
            TranspileStatementCheck("arg(u,<argv(0)>)", "local.u=<argv[0]>");
            TranspileStatementCheck("arg(u,<arg(u)>,<arg(v)>)", "local.u=<local.u>,<local.v>");
            TranspileStatementCheck("arg.u=0", "local.u=0");
            TranspileStatementCheck("arg.u=<arg.u>", "local.u=<local.u>");
            TranspileStatementCheck("equip(arg(hiditem))", "equip <local.hiditem>");

            TranspileCodeBlockCheck(@"arg(u,1)
arg(v,<eval u>)",
@"local.u=1
local.v=<eval <local.u>>");
        }

        [TestMethod]
        public void Self_reference_sharp_replacement()
        {
            TranspileStatementCheck("arg(u,#+1)", "local.u=<local.u>+1");
            TranspileStatementCheck("tag(u,#+1)", "tag.u=<tag.u>+1");
            TranspileStatementCheck("var(u,#+1)", "var.u=<var.u>+1");
            TranspileStatementCheck("var(u[arg(x)],#+1)", "var.u_<eval <local.x>>_=<var.u_<eval <local.x>>_>+1");
            TranspileStatementCheck("tag(u[arg(x)],#+1)", "tag.u_<eval <local.x>>_=<tag.u_<eval <local.x>>_>+1");
            TranspileStatementCheck("var(u[1],#+1)", "var.u_1_=<var.u_1_>+1");
            TranspileStatementCheck("tag(u[2],#+1)", "tag.u_2_=<tag.u_2_>+1");
            TranspileStatementCheck("src.tag(u,#+1)", "src.tag.u=<src.tag.u>+1");
        }

        [TestMethod]
        public void Variable_name_conflicts()
        {
            TranspileStatementCheck("arg(name,<src.name>)", "local.name=<src.name>");
            TranspileStatementCheck("var(name,<src.tag.name>)", "var.name=<src.tag.name>");

            TranspileFileCheck(@"[function fun1]
var(name,1)
src.tag(name,<src.tag.name>)",
@"[function fun1]
var.name=1
src.tag.name=<src.tag.name>");
        }

        [TestMethod]
        public void Local_variable_tag_name_conflict()
        {
            TranspileFileCheck(@"[function fun1]
arg(u,1)
arg(v,1)
tag.u=1
tag(u,1)
tag(u_<arg(v)>,1)",
@"[function fun1]
local.u=1
local.v=1
tag.u=1
tag.u=1
tag.u_<local.v>=1");
            TranspileFileCheck(@"[function fun1]
var(u,1)
var(v,1)
tag.u=1
tag(u,1)
tag(u_<var(v)>,1)",
@"[function fun1]
var.u=1
var.v=1
tag.u=1
tag.u=1
tag.u_<var.v>=1");
        }

        [TestMethod]
        public void Induced_uid()
        {
            TranspileStatementCheck("arg(v,<arg(u).name>)", "local.v=<uid.<local.u>.name>");
            TranspileStatementCheck("arg(v,<tag(detect_src).name>)", "local.v=<uid.<tag0.detect_src>.name>");
            TranspileStatementCheck("arg(v,<var(detect_src).name>)", "local.v=<uid.<var0.detect_src>.name>");

            TranspileConditionCheck("<eval arg(x).flags>", "<eval <uid.<local.x>.flags>>");
            TranspileConditionCheck("<eval tag(x).flags>", "<eval <uid.<tag0.x>.flags>>");
        }

        [TestMethod]
        public void Can_recognize_local_variable_read_access()
        {
            TranspileFileCheck(@"[function fun1]
arg(xxx,1)
arg(yyy,<eval <xxx>>)
yyy.color=1
equip(xxx)
equip(<xxx>)",
@"[function fun1]
local.xxx=1
local.yyy=<eval <local.xxx>>
local.yyy.color=1
equip <local.xxx>
equip <local.xxx>");
        }

        [TestMethod]
        public void Function_is_local_variable_scope()
        {
            TranspileFileCheck(@"[function fun1]
if 1
    arg(variable1,1)
else
    arg(variable1,2)
endif

variable1=<variable1>

[function fun2]
variable1
", @"[function fun1]
if 1
    local.variable1=1
else
    local.variable1=2
endif

local.variable1=<local.variable1>

[function fun2]
variable1
");
        }

        [TestMethod]
        public void Trigger_name_translation()
        {
            TranspileFileCheck(@"[itemdef i_item]
on=@UserDClick
",
@"[itemdef i_item]
on=@dclick

[function i_item]
return i_item");
        }

        [TestMethod]
        public void Trigger_is_local_variable_scope()
        {
            TranspileFileCheck(@"[itemdef i_item]
ID=i_memory

on=@trigger1
if 1
    arg(variable1,1)
endif
variable1=<variable1>",
@"[itemdef i_item]
ID=i_memory

on=@trigger1
if 1
    local.variable1=1
endif
local.variable1=<local.variable1>
[function i_item]
return i_item");
        }

        [TestMethod]
        public void Can_recognize_global_variable_read_access()
        {
            TranspileCodeBlockCheck(@"var(xxx,1)
var(yyy,<eval <xxx>>)",
@"var.xxx=1
var.yyy=<eval <var.xxx>>");

            TranspileCodeBlockCheck(@"var(asciitext,1)
var(asciitext,1)",
@"var.asciitext=1
var.asciitext=1");
        }

        [TestMethod]
        public void Tags()
        {
            TranspileStatementCheck("tag(name,value)", "tag.name=value");
            TranspileStatementCheck("arg(u,tag(name))", "local.u=tag.name");
            TranspileStatementCheck("tag.remove(u)", "tag.u=");
            TranspileStatementCheck("tag.remove(u[<arg(x)>])", "tag.u_<eval <local.x>>_=");
            TranspileStatementCheck("tag.u.remove", "tag.u=");
            TranspileStatementCheck("tag(name,value1,value2)", "tag.name=value1,value2");
            TranspileStatementCheck("tag(name[<tag(index)>],value)", "tag.name_<eval <tag0.index>>_=value");
            TranspileStatementCheck("link.timerd=<link.tag.hitspeed>", "link.timerd=<link.tag.hitspeed>");
            TranspileCodeBlockCheck(@"tag(u,1)
tag(v,<eval tag.u>)",
@"tag.u=1
tag.v=<eval <tag0.u>>");

            // TODO:
            //TranspileStatementCheck(
            //    "act.damagecust(<arg(celk_dam)>,<hval tag(weapflag)<tag(mi_weapflags)>>,<eval tag(piercing)+typedef.tag(piercing)>)",
            //    "act.damagecust <local.celk_dam>,<hval <tag.weapflag><tag.mi_weapflags>>,<eval <tag0.piercing>+<typedef.tag0.piercing>>");
        }

        [TestMethod]
        public void Global_variables()
        {
            TranspileStatementCheck("var(name,value)", "var.name=value");
            TranspileStatementCheck("arg(u,var(name))", "local.u=var.name");

            TranspileCodeBlockCheck(@"var(u,1)
var(v,<eval u>)",
@"var.u=1
var.v=<eval <var.u>>");

            TranspileCodeBlockCheck(
@"var(name_<argv(0)>,1)
var(name_<argv(0)>,2)",
@"var.name_<argv[0]>=1
var.name_<argv[0]>=2");

            TranspileCodeBlockCheck(
@"var(u,1)
fun1(u)",
@"var.u=1
fun1 <var.u>");

            TranspileCodeBlockCheck(
@"var(is_blunt    ,1)
var(u,<eval(is_blunt)>)",
@"var.is_blunt    =1
var.u=<eval(<var.is_blunt>)>");
        }

        [TestMethod]
        public void Defnames()
        {
            TranspileFileCheck(
@"[defnames defs1]
xy   1
[function fun1]
call(<xy>)
",
@"[defname defs1]
xy   1

[function xy]
return <def.xy>

[function fun1]
call <xy>
");

            TranspileFileCheck(
@"[defnames defs1]
xy[0]   1
[function fun1]
call(<xy[0]>)
call(<xy[<arg(i)>]>)
call(<xy[<eval arg(i)>]>)
",
@"[defname defs1]
xy_0_   1

[function xy_0_]
return <def.xy_0_>

[function fun1]
call <xy_0_>
call <xy_<eval <local.i>>_>
call <xy_<eval <local.i>>_>
");
        }

        [TestMethod]
        [DataRow("findlayer(layer_pack).remove", "findlayer.<layer_pack>.remove")]
        [DataRow("arg(u,findlayer(layer_pack))", "local.u=findlayer.<layer_pack>")]
        public void DottedArguments(string source, string expectedResult)
        {
            TranspileStatementCheck(source, expectedResult);
        }

        [TestMethod]
        public void Can_transpile_function()
        {
            TranspileFileCheck(
@"[function fun1]
call1
call2",

@"[function fun1]
call1
call2");
        }

        [TestMethod]
        public void TypeDefs_section()
        {
            TranspileFileCheck(@"[TYPEDEFS]
t_normal                  0
t_container               1",

@"[TYPEDEFS]
t_normal                  0
t_container               1
[function t_normal]
return 0

[function t_container]
return 1");
        }

        [TestMethod]
        public void DefNames_section()
        {
            TranspileFileCheck(@"[DEFNAMES blockedIPs section name with spaces]
d_blocked_ips        0",
@"[defname blockedIPs section name with spaces]
d_blocked_ips        0
[function d_blocked_ips]
return <def.d_blocked_ips>");
        }

        [TestMethod]
        public void Speech_section()
        {
            TranspileFileCheck(@"[SPEECH spk_human_prime]
ON=*
    call1

ON=*hire*
    call2

ON=*train*
ON=*teach*
    call3",
@"[SPEECH spk_human_prime]
ON=*
    call1

ON=*hire*
    call2

ON=*train*
ON=*teach*
    call3");
        }

        [TestMethod]
        public void Comment_section()
        {
            TranspileFileCheck(@"[COMMENT]
comment line 1
comment line 2
",
@"[COMMENT]
comment line 1
comment line 2

");
        }

        [TestMethod]
        public void Profession_section()
        {
            TranspileFileCheck(@"[profession 1]
DEFNAME=class_necro
NAME=Necro
STATSUM=1000
str=400
EI=1000
Resist=1000

on=@login
events e_character
",
@"[skillclass 1]
DEFNAME=class_necro
NAME=Necro
STATSUM=1000
str=400
EvaluatingIntel=100.0
MagicResistance=100.0

on=@login
events e_character

[function class_necro]
return class_necro
");
        }

        [TestMethod]
        public void Template_section()
        {
            TranspileFileCheck(@"[template tm_rangernewbie]
item=i_spellbook_3
ITEM=i_shirt_plain",
@"[template tm_rangernewbie]
item=i_spellbook_3
ITEM=i_shirt_plain");

            TranspileFileCheck(@"[template 1234]
defname=loot_something",
@"[template 1234]
defname=loot_something");
        }

        [TestMethod]
        public void Dialog_settext_function()
        {
            TranspileFileCheck(@"[dialog d_test]
argo.settext(10,""text"")
",
@"[dialog d_test]
0,0
setdialogtext d_test,10,""text""
");
        }

        [TestMethod]
        public void Dialog_textentry_function()
        {
            TranspileFileCheck(@"[dialog d_test]
argo.textentry(340,233,300,30,1000,100,10)
",
@"[dialog d_test]
0,0
dtextentry 340 233 300 30 1000 100 <getdialogtext d_test,10>
");

            TranspileFileCheck(@"[dialog d_test]
textentry 150 120 340 347 2116 1 1
",
@"[dialog d_test]
0,0
dtextentry 150 120 340 347 2116 1 <getdialogtext d_test,1>
");
        }

        [TestMethod]
        public void Dialog_functions()
        {
            TranspileStatementCheck("dialog(d_test)", "dialog d_test");
            TranspileStatementCheck("dialog(d_test,1)", "dialog d_test,0,1");
            TranspileStatementCheck("dialog(d_test,\"first arg\",\"second arg\")", "dialog d_test,0,\"first arg\",\"second arg\"");
            TranspileStatementCheck("button 170,210,2151,2152,1,0,1", "button 170 210 2151 2152 1 0 1");
            TranspileStatementCheck("argo.button 170,210,2151,2152,1,0,1", "button 170 210 2151 2152 1 0 1");
            TranspileStatementCheck("argo.HTMLGUMP 20,20,600,200,0,0,0", "htmlgump 20 20 600 200 0 0 0");
            TranspileStatementCheck("gumppic 140,200,2200", "gumppic 140 200 2200");
            TranspileStatementCheck("argo.texta(180,233,1000,\"text\")", "dtext 180 233 1000 text");
            TranspileStatementCheck("argo.textentry(340,233,300,30,1000,100,10)", "textentry 340 233 300 30 1000 100 10");
            TranspileStatementCheck(
                "HTMLGUMPa(210,215,110,160,\"some text\",0,0)",
                "dhtmlgump 210 215 110 160 0 0 some text");

            TranspileCodeBlockCheck(
@"HTMLGUMPa(210,215,110,160,<?std_basefont?><?seznamclass_0_?><?basefont_end?>,0,0) // comment
            ",
@"dhtmlgump 210 215 110 160 0 0 <std_basefont><seznamclass_0_><basefont_end> // comment
            ");
        }

        [TestMethod]
        public void Dialog_trigger_functions()
        {
            TranspileFileCheck(@"[dialog d_test button]
on=0
argo.tag(test)
",
@"[dialog d_test button]
on=0
src.tag.test
");
        }

        [TestMethod]
        public void Dialog_sections()
        {
            TranspileFileCheck(
@"[DIALOG D_RACEclass_background]
123,456
gumppic 510 110 5536
gumppic 35 110 5536

[DIALOG D_RACEclass_background button]
on=0
call1

on=@anybutton
call2

[DIALOG D_RACEclass_background text]
some text
",

@"[DIALOG D_RACEclass_background]
123,456
gumppic 510 110 5536
gumppic 35 110 5536

[DIALOG D_RACEclass_background button]
on=0
call1

on=0 255
call2

[DIALOG D_RACEclass_background text]
some text
");

            TranspileFileCheck(
@"[dialog d_dlg]
gumppic 510 110 5536
argo.SetLocation(285,250)",
@"[dialog d_dlg]
285,250
gumppic 510 110 5536");

            TranspileFileCheck(
@"[dialog d_dlg]
gumppic 510 110 5536
argo.SetLocation=285,250",
@"[dialog d_dlg]
285,250
gumppic 510 110 5536");

            TranspileFileCheck(
@"[dialog d_dlg]
gumppic 510 110 5536",
@"[dialog d_dlg]
0,0
gumppic 510 110 5536");
        }

        [TestMethod]
        public void Itemdef_section()
        {
            TranspileFileCheck(
@"[itemdef i_test]
name=some name",
@"[itemdef i_test]
name=some name
[function i_test]
return i_test");

            TranspileFileCheck(
@"[ITEMDEF 0469]
DEFNAME=i_test",
@"[ITEMDEF 0469]
DEFNAME=i_test
[function i_test]
return i_test");
        }

        [TestMethod]
        public void Chardef_section()
        {
            TranspileFileCheck(
@"[chardef c_test]
name=some name",
@"[chardef c_test]
name=some name");

            TranspileFileCheck(
@"[CHARDEF 0469]
DEFNAME=c_test",
@"[CHARDEF 0469]
DEFNAME=c_test");
        }


        [TestMethod]
        [DataRow("name=fullspawner (.x spawnfull)", "name=fullspawner (.x spawnfull)")]
        [DataRow("WEIGHT=", "WEIGHT=")]
        public void Property(string source, string expectedResult)
        {
            TranspilePropertyAssignmentCheck(source, expectedResult);
        }

        [TestMethod]
        public void Findres()
        {
            TranspileConditionCheck("<findres(chardef,tag.spawnID).defname>", "<serv.chardef.<tag.spawnID>.defname>");
            TranspileConditionCheck("<findres(chardef, tag.spawnID).defname>", "<serv.chardef.<tag.spawnID>.defname>");
            TranspileConditionCheck("<findres(skill,<arg(u)>).name>", "<serv.skill.<local.u>.name>");
            TranspileConditionCheck("<findres(spell,<args>).flags>", "<serv.spell.<args>.flags>");
            TranspileConditionCheck("findres(itemdef,arg(createID))", "<serv.itemdef.<local.createID>>");
            TranspileStatementCheck("arg(x,<findres(skill,arg(i)).name>)", "local.x=<serv.skill.<local.i>.name>");
        }

        [TestMethod]
        public void Finduid()
        {
            TranspileConditionCheck("finduid(tag(weaponuid))", "<uid.<tag.weaponuid>>");
            TranspileConditionCheck("finduid(tag(weaponuid)).more2", "<uid.<tag.weaponuid>.more2>");
        }

        [TestMethod]
        public void Findlayer()
        {
            TranspileConditionCheck("findlayer(tag(weaponuid).typedef.layer)", "<findlayer.<uid.<tag0.weaponuid>.typedef.layer>>");
            TranspileConditionCheck("findlayer.<args>", "<findlayer.<args>>");
        }

        private void TranspileStatementCheck(string input, string expectedOutput)
            => TranspileCheck(input, expectedOutput, (src, parser) => parser.ParseStatement(src));

        private void TranspileFileCheck(string input, string expectedOutput)
            => TranspileCheck(input, expectedOutput, (src, parser) => parser.ParseFile(src));

        private void TranspileConditionCheck(string input, string expectedOutput)
            => TranspileCheck(input, expectedOutput, (src, parser) => parser.ParseCondition(src));

        private void TranspileCodeBlockCheck(string input, string expectedOutput)
            => TranspileCheck(input, expectedOutput, (src, parser) => parser.ParseCodeBlock(src));

        private void TranspilePropertyAssignmentCheck(string input, string expectedOutput)
            => TranspileCheck(input, expectedOutput, (src, parser) => parser.ParsePropertyAssignment(src));

        private DefinitionsRepository definitionsRepository = new DefinitionsRepository();

        private void TranspileCheck(string input, string expectedOutput, Func<string, SphereSharp.Sphere99.Parser, ParsingResult> parseFunc)
        {
            var parser = new SphereSharp.Sphere99.Parser();
            var parsingOutput = parseFunc(input, parser);

            if (parsingOutput.Errors.Any())
            {
                Assert.Fail(parsingOutput.GetErrorsText());
            }

            new DefinitionsCollector(definitionsRepository).Visit(parsingOutput.Tree);

            var transpiler = new SphereSharp.Sphere99.Sphere56TranspilerVisitor(definitionsRepository);
            transpiler.Visit(parsingOutput.Tree);


            transpiler.Output.Trim().Should().Be(expectedOutput.Trim());
        }
    }
}
