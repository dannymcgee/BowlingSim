using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using System.Linq;

[TestFixture]
public class ScoreMasterTest {

	[Test]
	public void T00PassingTest() {
		Assert.AreEqual( 1, 1 );
	}

	[Test]
	public void T01Bowl23() {
		int[] rolls = { 2, 3 };
		int[] frames = { 5 };
		Assert.AreEqual( frames.ToList(), ScoreMaster.ScoreFrames( rolls.ToList() ) );
	}

	[Test]
	public void T02Bowl234() {
		int[] rolls = { 2, 3, 4 };
		int[] frames = { 5 };
		Assert.AreEqual( frames.ToList(), ScoreMaster.ScoreFrames( rolls.ToList() ) );
	}

	[Test]
	public void T03Bowl2345() {
		int[] rolls = { 2, 3, 4, 5 };
		int[] frames = { 5, 9 };
		Assert.AreEqual( frames.ToList(), ScoreMaster.ScoreFrames( rolls.ToList() ) );
	}

	[Test]
	public void T04Bowl23456() {
		int[] rolls = { 2, 3, 4, 5, 6 };
		int[] frames = { 5, 9 };
		Assert.AreEqual( frames.ToList(), ScoreMaster.ScoreFrames( rolls.ToList() ) );
	}

	[Test]
	public void T04Bowl234564() {
		int[] rolls = { 2, 3, 4, 5, 6, 4 };
		int[] frames = { 5, 9 };
		Assert.AreEqual( frames.ToList(), ScoreMaster.ScoreFrames( rolls.ToList() ) );
	}

	[Test]
	public void T05BowlX1() {
		int[] rolls = { 10, 1 };
		int[] frames = { };
		Assert.AreEqual( frames.ToList(), ScoreMaster.ScoreFrames( rolls.ToList() ) );
	}

	[Test]
	public void T06Bowl19() {
		int[] rolls = { 1, 9 };
		int[] frames = { };
		Assert.AreEqual( frames.ToList(), ScoreMaster.ScoreFrames( rolls.ToList() ) );
	}

	[Test]
	public void T07SpareBonus() {
		int[] rolls = { 1, 2, 3, 5, 5, 5, 3, 3 };
		int[] frames = { 3, 8, 13, 6 };
		Assert.AreEqual( frames.ToList(), ScoreMaster.ScoreFrames( rolls.ToList() ) );
	}

	[Test]
	public void T08Bowl2345648() {
		int[] rolls = { 2, 3, 4, 5, 6, 4, 8 };
		int[] frames = { 5, 9, 18 };
		Assert.AreEqual( frames.ToList(), ScoreMaster.ScoreFrames( rolls.ToList() ) );
	}

	[Test]
	public void T09SpareBonus2() {
		int[] rolls = { 1, 2, 3, 5, 5, 5, 3, 3, 7, 1, 9, 1, 6 };
		int[] frames = { 3, 8, 13, 6, 8, 16 };
		Assert.AreEqual( frames.ToList(), ScoreMaster.ScoreFrames( rolls.ToList() ) );
	}

	[Test]
	public void T10StrikeBonus() {
		int[] rolls = { 10, 3, 4 };
		int[] frames = { 17, 7 };
		Assert.AreEqual( frames.ToList(), ScoreMaster.ScoreFrames( rolls.ToList() ) );
	}

	[Test]
	public void T11StrikeBonus3() {
		int[] rolls = { 1, 2, 3, 4, 5, 4, 3, 2, 10, 1, 3, 3, 4 };
		int[] frames = { 3, 7, 9, 5, 14, 4, 7 };
		Assert.AreEqual( frames.ToList(), ScoreMaster.ScoreFrames( rolls.ToList() ) );
	}

	[Test]
	public void T12MultiStrikes() {
		int[] rolls = { 10, 10, 10, 10, 10 };
		int[] frames = { 30, 30, 30 };
		Assert.AreEqual( frames.ToList(), ScoreMaster.ScoreFrames( rolls.ToList() ) );
	}

	[Test]
	public void T13GutterGame() {
		int[] rolls = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
		int[] frames = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
		Assert.AreEqual( frames.ToList(), ScoreMaster.ScoreFrames( rolls.ToList() ) );
	}

	[Test]
	public void T13GutterGameCumulative() {
		int[] rolls = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
		int[] totals = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
		Assert.AreEqual( totals.ToList(), ScoreMaster.ScoreCumulative( rolls.ToList() ) );
	}

	[Test]
	public void T14PerfectGame() {
		int[] rolls = { 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10 };
		int[] frames = { 30, 30, 30, 30, 30, 30, 30, 30, 30, 30 };
		Assert.AreEqual( frames.ToList(), ScoreMaster.ScoreFrames( rolls.ToList() ) );
	}

	[Test]
	public void T14PerfectGameCumulative() {
		int[] rolls = { 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10 };
		int[] frames = { 30, 60, 90, 120, 150, 180, 210, 240, 270, 300 };
		Assert.AreEqual( frames.ToList(), ScoreMaster.ScoreCumulative( rolls.ToList() ) );
	}

	[Test]
	public void T15StrikeInLastFrame() {
		int[] rolls = { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 10, 2, 3 };
		int[] frames = { 2, 4, 6, 8, 10, 12, 14, 16, 18, 33 };
		Assert.AreEqual( frames.ToList(), ScoreMaster.ScoreCumulative( rolls.ToList() ) );
	}



}
