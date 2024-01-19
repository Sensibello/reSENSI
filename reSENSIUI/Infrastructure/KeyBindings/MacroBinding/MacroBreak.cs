using System;
using reSENSICommon.Infrastructure.Enums;
using XBEliteWPF.Infrastructure.KeyBindingsModel.MacroBinding;

namespace reSENSIUI.Infrastructure.KeyBindings.MacroBinding
{
	public class MacroBreak : BaseMacro
	{
		public override MacroItemType MacroItemType
		{
			get
			{
				return 7;
			}
		}

		public MacroBreak(MacroSequence macroSequence)
			: base(macroSequence)
		{
		}

		public override BaseMacro Clone(MacroSequence hostMacroSequence)
		{
			return new MacroBreak(hostMacroSequence);
		}

		public override BaseMacro Clone(MacroSequence hostMacroSequence)
		{
			return new MacroBreak();
		}
	}
}
