using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using System.Linq;

[TestFixture]
public class ScoreDisplayTest {

	[Test]
	public void T00PassingTest() {
		Assert.AreEqual( 1, 1 );
	}

	[Test]
	public void T01Bowl1() {
		int[] rolls = { 1 };
		string rollsString = "1";
		Assert.AreEqual( rollsString, ScoreDisplay.FormatRolls( rolls.ToList() ) );
	}

	[Test]
	public void T02StrikeSymbolReplace() {
		int[] rolls = { 10 };
		string rollsString = "X ";
		Assert.AreEqual( rollsString, ScoreDisplay.FormatRolls( rolls.ToList() ) );
	}

	[Test]
	public void T03StrikeThen9() {
		int[] rolls = { 10, 9 };
		string rollsString = "X 9";
		Assert.AreEqual( rollsString, ScoreDisplay.FormatRolls( rolls.ToList() ) );
	}

	[Test]
	public void T04Turkey() {
		int[] rolls = { 10, 10, 10 };
		string rollsString = "X X X ";
		Assert.AreEqual( rollsString, ScoreDisplay.FormatRolls( rolls.ToList() ) );
	}

	[Test]
	public void T05TurkeyThenZeroSpare() {
		int[] rolls = { 10, 10, 10, 0, 10 };
		string rollsString = "X X X -/";
		Assert.AreEqual( rollsString, ScoreDisplay.FormatRolls( rolls.ToList() ) );
	}

	[Test]
	public void T06SomeBasicRolls() {
		int[] rolls = { 3, 2, 5, 5, 7, 2, 10 };
		string rollsString = "325/72X ";
		Assert.AreEqual( rollsString, ScoreDisplay.FormatRolls( rolls.ToList() ) );
	}

	[Test]
	public void T07LastFrameSpecialCase1() {
		int[] rolls = { 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10 };
		string rollsString = "X X X X X X X X X XXX";
		Assert.AreEqual( rollsString, ScoreDisplay.FormatRolls( rolls.ToList() ) );
	}

	[Test]
	public void T08LastFrameSpecialCase2() {
		int[] rolls = { 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 8, 2 };
		string rollsString = "X X X X X X X X X X8/";
		Assert.AreEqual( rollsString, ScoreDisplay.FormatRolls( rolls.ToList() ) );
	}

	[Test]
	public void T09LastFrameSpecialCase3() {
		int[] rolls = { 10, 10, 10, 10, 10, 10, 10, 10, 10, 8, 2, 5 };
		string rollsString = "X X X X X X X X X 8/5";
		Assert.AreEqual( rollsString, ScoreDisplay.FormatRolls( rolls.ToList() ) );
	}

	[Test]
	public void T10RandomRolls() {
		int[] rolls = {
			8,
			0,
			6,
			1,
			3,
			6,
			4,
			4,
			6,
			1,
			9,
			1,
			8,
			2,
			4,
			2,
			1,
			5,
			10,
			10,
			8
		};
		string rollsString = "8-613644619/8/4215XX8";
		Assert.AreEqual( rollsString, ScoreDisplay.FormatRolls( rolls.ToList() ) );
	}

	[Test]
	public void T11GoldenCopy1() {
		int[] rolls = { 10, 10, 10, 10, 9, 0, 10, 10, 10, 10, 10, 9, 1 };
		string rollsString = "X X X X 9-X X X X X9/";
		Assert.AreEqual( rollsString, ScoreDisplay.FormatRolls( rolls.ToList() ) );
	}

}