using System;
using NUnit.Framework;
using NoteApp;

namespace NoteApp.UnitTests
{
    [TestFixture]
    public class NoteTests
    {
        private Note _note;

        [SetUp]
        public void Setup()
        {
            _note=new Note();
        }

        [Test(Description = "���� �������� ���: �������� ����������� ��������� �������� �������.")]
        public void TestNameGet_CorrectValue()
        {
            _note.Name = "���������� � ����� ��� ���������.";
            var actual = _note.Name;
            Assert.AreEqual(_note.Name, actual ,"������ ���������� ������������ �������� �������.");
        }

        [Test(Description = "���� �������� ���: �������� ���������� ����������� �������� �������.")]
        public void TestNameSet_CorrectValue()
        {
            var expected = "���������� � ����� ��� ���������";
            _note.Name = expected;
            Assert.AreEqual(expected, _note.Name,"������ ����������� ������������ �������� �������.");

        }

        [Test(Description = "���� �������� ���: �������� ���������� ������� �������� ������� � ������� ������������ �������� '��� ��������'.")]
        public void TestNameSet_EmptyString()
        {
            var expected = "��� ��������";
            _note.Name = "";
            Assert.AreEqual(expected, _note.Name, "������ �� ������ ����������� �������� ������ ������ �������� �������.");
        }

        [Test(Description = "���� �������� ���: ������ ���������� ��� ���������� �������� ������� ������� ����� 50 ��������.")]
        public void TestNameSet_ArgumentException()
        {
            var WrongName = "12345678901234567890123456789012345678901234567890123";
            Assert.Throws<ArgumentException>((() => { _note.Name = WrongName;}),"� ������� �� ���������� ������������ ArgumentException.");
        }

        [Test(Description = "���� �������� ���������: �������� ����������� �������� ��������� �������.")]
        public void TestCategoryGet_CorrectValue()
        {
            _note.Category = NoteCategory.Home;
            var actual = _note.Category;
            Assert.AreEqual(_note.Category, actual,"������ ���������� ������������ ���������.");
        }

        [Test(Description = "���� �������� ���������: �������� ���������� �������� ��������� �������.")]
        public void TestCategorySet_CorrectValue()
        {
            var expected = NoteCategory.Home;
            _note.Category = expected;
            Assert.AreEqual(expected,_note.Category,"������ ����������� ������������ ���������.");
        }

        [Test(Description = "���� �������� �����: �������� ����������� �������� ������ ������ �������.")]
        public void TestTextGet_CorrectValue()
        {
            _note.Text = "�����";
            var actual = _note.Text;
            Assert.AreEqual(_note.Text,actual,"������ ���������� ������������ ������ ������ �������.");
        }

        [Test(Description = "���� �������� �����: �������� ���������� �������� ������ ������ �������.")]
        public void TestTextSet_CorrectValue()
        {
            var expected = _note.Text;
            _note.Text = expected;
            Assert.AreEqual(expected,_note.Text,"������ ����������� ������������ ������ ������ �������.");
        }

        [Test(Description = "���� �������� �����: �������� ���������� ������ ������ ������ ������� � ������� ������������ �������� '������� �����.'.")]
        public void TestTextSet_EmptyString()
        {
            var expected = "������� �����.";
            _note.Text = "";
            Assert.AreEqual(expected, _note.Text, "������ �� ������ ����������� �������� ������ ������.");
        }

        [Test(Description = "���� �������� ����� �������� �������: �������� ������������ ����������� ����� ��������.")]
        public void TestCreatedGet_CorrectValue()
        {
            var expected = DateTime.Now;
            expected=new DateTime(expected.Year, expected.Month, expected.Day, expected.Hour, expected.Minute, expected.Second, expected.Kind);
            var actual = _note.Created;
            actual=new DateTime(actual.Year, actual.Month, actual.Day, actual.Hour, actual.Minute, actual.Second, actual.Kind);
            Assert.AreEqual(expected, actual,"������ ���������� �� ���������� ����� �������� �������.");
        }

        /*
        [Test(Description = "���� �������� ����� �������� �������: �������� ������������ ")]
        public void TestCreatedSet_CorrectValue()
        {
            Assert.Pass();
        }
        */

        [Test(Description = "���� �������� ����� �������������� �������: �������� ������������ ����������� ����� ��������������.")]
        public void TestModifiedGet_CorrectValue()
        {
            _note.Name = "������ ��������.";
            var FirstEditTime = _note.Modified;
            _note.Name = "������ ��������.";
            var SecondEditTime = _note.Modified;
            Assert.AreNotEqual(FirstEditTime, SecondEditTime, "������ �� ����������� ������ ����� ��������� �������.");
        }

        /*
        [Test(Description = "���� �������� ����� �������������� �������: �������� ������������ ���������� ����� ��������������.")]
        public void TestModifiedSet_CorrectValue()
        {
            _note.Name = "������ ��������.";
            var FirstEditTime = _note.Modified;
            _note.Name = "������ ��������.";
            var SecondEditTime = _note.Modified;
            Assert.AreNotEqual(FirstEditTime,SecondEditTime,"");
        }
        */

    }
}