﻿using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SphereSharp.Sphere99.Sphere56Transpiler
{
    public sealed class WorldTranspiler : sphereScript99BaseVisitor<bool>
    {
        private readonly Sphere56TranspilerVisitor dataVisitor;
        private readonly SourceCodeBuilder dataBuilder = new SourceCodeBuilder();
        private readonly Sphere56TranspilerVisitor worldVisitor;
        private readonly SourceCodeBuilder worldBuilder = new SourceCodeBuilder();

        public WorldTranspiler(IDefinitionsRepository definitionsRepository = null)
        {
            dataVisitor = new Sphere56TranspilerVisitor(definitionsRepository ?? new DefinitionsRepository(), dataBuilder);
            worldVisitor = new Sphere56TranspilerVisitor(definitionsRepository ?? new DefinitionsRepository(), worldBuilder);
        }

        public override bool VisitPropertyList([NotNull] sphereScript99Parser.PropertyListContext context)
        {
            return worldVisitor.Visit(context);
        }

        public override bool VisitWorldCharSection([NotNull] sphereScript99Parser.WorldCharSectionContext context)
        {
            return worldVisitor.VisitWorldCharSection(context);
        }

        public override bool VisitWorldItemSection([NotNull] sphereScript99Parser.WorldItemSectionContext context)
        {
            return worldVisitor.VisitWorldItemSection(context);
        }

        public override bool VisitSectorSectionHeader([NotNull] sphereScript99Parser.SectorSectionHeaderContext context)
        {
            return worldVisitor.VisitSectorSectionHeader(context);
        }

        public override bool VisitVarNamesSection([NotNull] sphereScript99Parser.VarNamesSectionContext context)
        {
            return dataVisitor.VisitVarNamesSection(context);
        }

        public WorldTranspilationResult Transpile(IParseTree parseTree)
        {
            Visit(parseTree);

            return new WorldTranspilationResult(worldBuilder.Output, dataBuilder.Output);
        }

    }
}