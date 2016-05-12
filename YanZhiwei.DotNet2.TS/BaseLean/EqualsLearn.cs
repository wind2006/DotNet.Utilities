using System;

namespace YanZhiwei.DotNet2.TS.BaseLean
{
    public class EqualsLearn
    {
        public void Demo1()
        {
            Team _team1 = new Team("YanZhiwei");
            Team _team2 = new Team("YanZhiwei");
            Console.WriteLine("1._team1 == _team2:" + (_team1 == _team2));
            Console.WriteLine("2._team1.Equals(_team2):" + (_team1.Equals(_team2)));
            Console.WriteLine("3.object.ReferenceEquals(_team1,_team2):" + (object.ReferenceEquals(_team1, _team2)));
            //引用类型比较的是引用地址；_team1，_team2 指向不同对象的示例，所以均返回False;

            TeamStruct _teamstruct1 = new TeamStruct("YanZhiwei");
            TeamStruct _teamstruct2 = new TeamStruct("YanZhiwei");
            //Console.WriteLine(_teamstruct1 == _teamstruct2); 值类型不能用==比较
            Console.WriteLine("4._teamstruct1.Equals(_teamstruct2):" + _teamstruct1.Equals(_teamstruct2));

            NationalTeam _nTeam1 = new NationalTeam(_team1, _teamstruct1);
            NationalTeam _nTeam2 = new NationalTeam(_team2, _teamstruct2);
            NationalTeam _nTeam3 = _nTeam1;

            Console.WriteLine("5._nTeam1.Equals(_nTeam2):" + _nTeam1.Equals(_nTeam2));
            Console.WriteLine("6._nTeam1.Equals(_nTeam3):" + _nTeam1.Equals(_nTeam3));
            //遍历引用类型成员和值类型成员；5.中引用类型成员不是同一个对象实例；
        }
    }

    public class NationalTeam
    {
        public Team _team;
        public TeamStruct _structTeam;

        public NationalTeam(Team team, TeamStruct structTeam)
        {
            this._team = team;
            this._structTeam = structTeam;
        }
    }

    public class Team
    {
        public string _coach = string.Empty;

        public Team(string coach)
        {
            this._coach = coach;
        }
    }

    public struct TeamStruct
    {
        public string _coach;

        public TeamStruct(string coach)
        {
            this._coach = coach;
        }
    }
}