using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DotFuzzy;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WacanFuzzyLogic
{
    public partial class Form1 : Form
    {
        FuzzyEngine fe;
        MembershipFunctionCollection currentTemp, targetTemp, output;
        LinguisticVariable myCurrent, myTarget, myOutput;
        FuzzyRuleCollection myrules;


        public Form1()
        {
            InitializeComponent();
        }


        public void setMembers()
        {

            currentTemp = new MembershipFunctionCollection();
            currentTemp.Add(new MembershipFunction("LOW", 0.0, 0.0, 45.0, 50.0));
            currentTemp.Add(new MembershipFunction("LM", 0.0, 0.0, 45.0, 50.0));
            currentTemp.Add(new MembershipFunction("MEDIUM", 45.0, 50.0, 50.0, 55.0));
            currentTemp.Add(new MembershipFunction("HM", 0.0, 0.0, 45.0, 50.0));
            currentTemp.Add(new MembershipFunction("HIGH", 50.0, 55.0, 100.0, 100.0));
            myCurrent = new LinguisticVariable("CURRENT", currentTemp);


            targetTemp = new MembershipFunctionCollection();
            targetTemp.Add(new MembershipFunction("LOW", 0.0, 0.0, 45.0, 50.0));
            targetTemp.Add(new MembershipFunction("LM", 0.0, 0.0, 45.0, 50.0));
            targetTemp.Add(new MembershipFunction("MEDIUM", 45.0, 50.0, 50.0, 55.0));
            targetTemp.Add(new MembershipFunction("HM", 0.0, 0.0, 45.0, 50.0));
            targetTemp.Add(new MembershipFunction("HIGH", 50.0, 55.0, 100.0, 100.0));
            myTarget = new LinguisticVariable("TARGET", targetTemp);

            output = new MembershipFunctionCollection();
            output.Add(new MembershipFunction("LOW", 0.0, 0.0, 2.0, 4.0));
            output.Add(new MembershipFunction("LM", 2.0, 4.0, 4.0, 6.0));
            output.Add(new MembershipFunction("MED", 4.0, 6.0, 6.0, 8.0));
            output.Add(new MembershipFunction("HM", 6.0, 8.0, 8.0, 10.0));
            output.Add(new MembershipFunction("HIGH", 8.0, 10.0, 10.0, 10.0));
            myOutput = new LinguisticVariable("OUTPUT", output);



        }

        public void setRules()
        {
            myrules = new FuzzyRuleCollection();
            myrules.Add(new FuzzyRule("IF (CURRENT IS HIGH) AND (TARGET IS UP) THEN OUTPUT IS LM"));
            myrules.Add(new FuzzyRule("IF (CURRENT IS HIGH) AND (TARGET IS LEVEL) THEN OUTPUT IS LM"));
            myrules.Add(new FuzzyRule("IF (CURRENT IS HIGH) AND (TARGET IS DOWN) THEN OUTPUT IS LOW"));
            myrules.Add(new FuzzyRule("IF (CURRENT IS OK) AND (TARGET IS UP) THEN OUTPUT IS HM"));
            myrules.Add(new FuzzyRule("IF (CURRENT IS OK) AND (TARGET IS LEVEL) THEN OUTPUT IS MED"));
            myrules.Add(new FuzzyRule("IF (CURRENT IS OK) AND (TARGET IS DOWN) THEN OUTPUT IS LM"));
            myrules.Add(new FuzzyRule("IF (CURRENT IS LOW) AND (TARGET IS UP) THEN OUTPUT IS HIGH"));
            myrules.Add(new FuzzyRule("IF (CURRENT IS OK) AND (TARGET IS LEVEL) THEN OUTPUT IS HM"));
            myrules.Add(new FuzzyRule("IF (CURRENT IS OK) AND (TARGET IS DOWN) THEN OUTPUT IS HM"));
        }

        public void setFuzzyEngine()
        {
            fe = new FuzzyEngine();
            fe.LinguisticVariableCollection.Add(myCurrent);
            fe.LinguisticVariableCollection.Add(myTarget);
            fe.LinguisticVariableCollection.Add(myOutput);
            fe.FuzzyRuleCollection = myrules;
        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void defuziffyToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            setMembers();
            setRules();
            //setFuzzyEngine();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            myCurrent.InputValue = (Convert.ToDouble(textBox1.Text));
            myCurrent.Fuzzify("OK");



        }
        private void button2_Click(object sender, EventArgs e)
        {
            myTarget.InputValue = (Convert.ToDouble(textBox2.Text));
            myTarget.Fuzzify("LEVEL");
        }

        public void fuziffyvalues()
        {
            myCurrent.InputValue = (Convert.ToDouble(textBox1.Text));
            myCurrent.Fuzzify("LOW");
            myTarget.InputValue = (Convert.ToDouble(textBox2.Text));
            myTarget.Fuzzify("DOWN");

        }
        public void defuzzy()
        {
            setFuzzyEngine();
            fe.Consequent = "OUTPUT";
            textBox3.Text = "" + fe.Defuzzify();
        }
        public void computenewspeed()
        {

            double oldCurrent = Convert.ToDouble(textBox1.Text);
            double oldOutput = Convert.ToDouble(textBox3.Text);
            double oldTarget = Convert.ToDouble(textBox2.Text);
            double newCurrent = ((1 - 0.1) * (oldCurrent)) + (oldOutput - (0.1 * oldTarget));
            textBox1.Text = "" + newCurrent;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            setFuzzyEngine();
            fe.Consequent = "OUTPUT";
            textBox3.Text = "" + fe.Defuzzify();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            computenewspeed();
        }



        private void Form1_Load(object sender, EventArgs e)
        {
            setMembers();
            setRules();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            fuziffyvalues();
            defuzzy();
            computenewspeed();
        }


    }
}
