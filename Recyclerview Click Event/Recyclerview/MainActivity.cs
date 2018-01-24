using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

using Android.Support.V7.Widget;
using System.Collections.Generic;

namespace Recyclerview
{
    [Activity(Label = "Recyclerview", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        private RecyclerView recyclerview;

        RecyclerView.LayoutManager recyclerview_layoutmanger;
        private RecyclerView.Adapter recyclerview_adapter;
        private ContactListAdapter<ContactList> ContactListitems;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);          
            SetContentView(Resource.Layout.Main);
            recyclerview = FindViewById<RecyclerView>(Resource.Id.recyclerview);
            
            List<ContactList> contact = new List<ContactList>();
            contact.Add(new ContactList { ContactName = "Anbu", Number = "2564125624" });
            contact.Add(new ContactList { ContactName = "Arun", Number = "52459878788" });
            contact.Add(new ContactList { ContactName = "John", Number= "57879877878" });
            contact.Add(new ContactList { ContactName = "Peter", Number = "56478989798" });
            contact.Add(new ContactList { ContactName = "Ali", Number = "12345687945" });

            ContactListitems = new ContactListAdapter<ContactList>(); 

            foreach(var s in contact)
            {
                ContactListitems.Add(s);

            }            
            recyclerview_layoutmanger = new LinearLayoutManager(this, LinearLayoutManager.Vertical, false);
            recyclerview.SetLayoutManager(recyclerview_layoutmanger);
            recyclerview_adapter = new RecyclerAdapter(ContactListitems,recyclerview);
            recyclerview.SetAdapter(recyclerview_adapter);
        }
        public class RecyclerAdapter : RecyclerView.Adapter
        {
            private ContactListAdapter<ContactList> Mitems;

            Context context;
            RecyclerView mrecyle;
            public RecyclerAdapter(ContactListAdapter<ContactList> Mitems, RecyclerView recyler)
            {
                this.Mitems = Mitems;
                NotifyDataSetChanged();

                mrecyle = recyler;
            }
            public class MyView : RecyclerView.ViewHolder
            {
                public View mainview { get; set; }
                public TextView mtxtcontactname { get; set; }
                public TextView mtxtcontactnumber { get; set; }                
                public EventHandler ClickHandler { get; set; }  
                             
                public MyView(View view) : base(view)
                {
                    mainview = view;
                                    }

            }
            public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
            {
                View listitem = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.ContactLayout, parent, false);
                TextView txtcontactname = listitem.FindViewById<TextView>(Resource.Id.txtcontactname);
                TextView txtnumber = listitem.FindViewById<TextView>(Resource.Id.txtnumber);
                MyView view = new MyView(listitem) { mtxtcontactname = txtcontactname, mtxtcontactnumber = txtnumber};                
                return view;
            }
            public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
            {
                MyView myholder = holder as MyView;
                myholder.mtxtcontactname.Text = Mitems[position].ContactName;
                myholder.mtxtcontactnumber.Text = Mitems[position].Number;

                myholder.mainview.Click += Mainview_Click; 
            }

            private void Mainview_Click(object sender, EventArgs e)
            {
                int position = mrecyle.GetChildAdapterPosition((View)sender);
                //int indexPosition = (Mitems.Count-0) - position;
                Console.WriteLine(Mitems[position].ContactName);
               //D
            }

            public override int ItemCount
            {
                get
                {
                    return Mitems.Count;

                }
            }

        }

    }
}

