package com.example.user.proba;

import android.app.Activity;
import android.app.AlertDialog;
import android.content.BroadcastReceiver;
import android.content.ContentUris;
import android.content.ContentValues;
import android.content.DialogInterface;
import android.content.pm.ActivityInfo;
import android.database.sqlite.SQLiteDatabase;
import android.os.Bundle;
import android.os.Environment;
import android.os.Handler;
import android.os.Message;
import android.provider.MediaStore;
import android.support.annotation.NonNull;
import android.text.Editable;
import android.text.Html;
import android.text.TextWatcher;
import android.util.Log;
import android.view.ContextMenu;
import android.view.Gravity;
import android.view.KeyEvent;
import android.view.View;
import android.support.design.widget.NavigationView;
import android.support.v4.view.GravityCompat;
import android.support.v4.widget.DrawerLayout;
import android.support.v7.app.ActionBarDrawerToggle;
import android.support.v7.widget.Toolbar;
import android.view.Menu;
import android.view.MenuItem;
import android.view.Window;
import android.view.inputmethod.InputMethodManager;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.CheckBox;
import android.widget.EditText;
import android.widget.GridLayout;
import android.widget.ImageView;
import android.widget.LinearLayout;
import android.widget.ListView;
import android.widget.SeekBar;
import android.widget.Spinner;
import android.widget.Switch;
import android.widget.TextView;
import android.widget.Toast;
import android.database.sqlite.SQLiteDatabase;
import android.database.sqlite.SQLiteOpenHelper;
import android.os.Bundle;


import java.io.File;
import java.io.FileInputStream;
import java.io.FileNotFoundException;
import java.io.FileOutputStream;
import java.io.IOException;
import java.io.InputStream;
import java.io.OutputStream;
import java.lang.reflect.Array;
import java.nio.channels.FileChannel;
import java.security.spec.ECField;
import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.Calendar;
import java.util.Collection;
import java.util.Collections;
import java.util.Comparator;
import java.util.Date;
import java.util.HashMap;
import java.util.HashSet;
import java.util.Iterator;
import java.util.LinkedHashSet;
import java.util.List;
import java.util.ListIterator;
import java.util.Map;
import java.util.Set;
import android.net.Uri;
import android.content.ContentResolver;
import android.database.Cursor;
import android.os.IBinder;
import android.content.ComponentName;
import android.content.Intent;
import android.content.ServiceConnection;
import android.content.Context;

import com.example.user.proba.MusicService.MusicBinder;
import android.widget.MediaController.MediaPlayerControl;

public class MainActivity extends Activity implements MediaPlayerControl {
    private static final int MY_PERMISSION_REQUEST = 1;
    final String LOG_TAG = "myLogs";
    static Track Songer, redact_track;
    static int totalTime, active_track_int, redacter;
    Spinner Janr_sp, podjanr_box, lang_box, nastr_box, Sp_teg_type, Sp_teg_value, Sp_sort_value;
    static String Janr, Lang_, PodJanr, Nastr, uri_create, types_playlist_teg;
    static int Track_Teg, Playlist_key;
    EditText editText, editText2;
    ArrayAdapter<String> adapter;
    Track TMP;
    GridLayout Into_Track_layout;
    LinearLayout Create_track_list, Nastroyki,
            Active_layout_track, Create_New_track_list,
            Create_New_track_list_artist, Create_New_track_list_janr,
            Create_New_track_list_name, Create_New_track_list_podjanr,
            Create_New_track_list_lang, Create_New_track_list_nastr,
            Player_list, Playlists,
            Create_Playlist_menu, Create_Playlist_menu_teg_type,
            Create_Playlist_menu_teg_value, Create_Playlist_menu_classic_name;
    CheckBox Classic, Meloman;
    Button Nav_trackBTN, playBtn, pustishka, repeat, loveBtn;
    SeekBar positionBar;
    TextView elapsedTimeLabel, remainingTimeLabel, artist_box, artist_box1, artist_box2, artist_box3, artist_box4, artist_box5,
            name_box, LabelInfoArtist1, LabelInfoArtist2, LabelInfoArtist3, LabelInfoArtist4, LabelInfoTitle, LabelInfoJanr, LabelInfoPodJanr, LabelInfoLang,
            LabelInfoNastr,LabelInfoDate;
    String[] arraySpinner = new String[16], arraySpinner_lang = new String[8], arraySpinner_podjanr, arraySpinnerTeg_2,
        arraySpiner_create_list = {"По исполнителю","По жанру", "По поджанру", "По языку вокала", "По настроению" },
        arraySpiner_sort_collection = {"По исполнителю", "По названию", "По дате добавления"},
        arrayNastroenie_list = { "Безмятежность",  "Бодрость", "Вдохновение",  "Возмущение", "Волнение", "Воодушевление",  "Грусть", "Драйв",  "Злость",  "Интерес", "Ирония",  "Ликование",  "Надежда",  "Напряжение", "Негодование", "Нежность", "Нетерпение", "Обида",  "Озорство",  "Печаль",  "Подавленность",  "Порыв",  "Предвкушение",  "Радость",  "Скорбь", "Спокойствие",  "Стремление",  "Тревога",  "Удовлетворенность", "Умиротворение",  "Упорство",  "Энергичность", "Энтузиазм", "Ярость"};
    int repeat_;
    private ArrayList<Track>  AllTrackList, ActiveTrackList, PlayTrackList, CollectionTrackList, ListCollectionTeg, loveTrackCollection;
    static private  ArrayList<Track>  Clone;
    private ArrayList<Lang> Language, Janr_list, Podjanr_list;
    private ArrayList<Playlist> NamesPlaylist;
    DBHelper dbHelper;
    private ListView AllTrackListView, ActiveTrackListView, list_playlist;
    private MusicService musicSrv;
    private Intent playIntent;
    private boolean musicBound=false, love= false;
    private MusicController controller;
    private boolean paused=false, playbackPaused=false;
    TextView titleBox;
    private int pauseSong = 0;
    private ServiceConnection musicConnection;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        try {
            Playlist_key = 0;
            Log.d(LOG_TAG,"плеер включен");
            setRequestedOrientation(ActivityInfo.SCREEN_ORIENTATION_PORTRAIT);
            super.onCreate(savedInstanceState);
            setContentView(R.layout.activity_main);
            Playlists = (LinearLayout)findViewById(R.id.Playlists);
            final  TextView Create_Playlist = (TextView) findViewById(R.id.Create_Playlist);
            loveTrackCollection = new ArrayList<Track>();
            ListCollectionTeg = new ArrayList<Track>();
            Clone = new ArrayList<Track>();
            NamesPlaylist = new ArrayList<Playlist>();
            Toolbar toolbar = (Toolbar) findViewById(R.id.toolbar);
            setController();
            Perem();
            final LinearLayout bg = (LinearLayout)findViewById(R.id.Player);
            Into_Track_layout = (GridLayout)findViewById(R.id.Into_Track_layout);
            registerForContextMenu(bg);
            DrawerLayout drawer = (DrawerLayout) findViewById(R.id.drawer_layout);
            ActionBarDrawerToggle toggle = new ActionBarDrawerToggle(
                    this, drawer, toolbar, R.string.navigation_drawer_open, R.string.navigation_drawer_close);
            drawer.addDrawerListener(toggle);
            toggle.syncState();
            repeat_ = 0;
            try {
                CollectionTrackList = read("mytable");
            }
            catch (Exception e){
                Log.d(LOG_TAG,"Ошибка при чтении базы коллекции ");
            }
            try {
                    loveTrackCollection = read("mytable_1");
            }
            catch (Exception e){
                Log.d(LOG_TAG,"Ошибка при чтении базы Избранных " + e.getMessage());
            }
            getSongList();
            Collections.sort(AllTrackList, new Comparator<Track>(){
                public int compare(Track a, Track b){
                    return a.getTitle().compareTo(b.getTitle());
                }
            });
            positionBar = (SeekBar) findViewById(R.id.possitionBar);
            bg.setOnTouchListener(new OnSwipeTouchListener(this) {
                public void onSwipeTop() {

                }
                public void onSwipeRight() throws IOException {
                    musicSrv.playPrev();
                    playBtn.setBackgroundResource(R.drawable.stop);
                    players();
                }
                public void onSwipeLeft() throws IOException {
                    musicSrv.playNext();
                    playBtn.setBackgroundResource(R.drawable.stop);
                    players();
                }
                public void onSwipeBottom() {

                }
            });
            try {
                if(CollectionTrackList.size()>0){
                    ActiveTrackList = CollectionTrackList;
                    //ActiveTrackList = AllTrackList;
                }
                else {
                    ActiveTrackList = AllTrackList;
                }
            }catch (Exception e){
                Log.d(LOG_TAG, "Ошибка в передаче активному листу: " + e.getMessage());
            }
            SongAdapter songAllAdt = new SongAdapter(this, AllTrackList);
            AllTrackListView.setAdapter(songAllAdt);
            SongAdapterActiveList songActAdt = new SongAdapterActiveList(this, ActiveTrackList);
            ActiveTrackListView.setAdapter(songActAdt);
            SongList();

            Playlist t = new Playlist("Все песни",AllTrackList);
            NamesPlaylist.add(t);
            Log.d(LOG_TAG, "Размер коллекции: "+CollectionTrackList.size());
            t = new Playlist("Коллекция",CollectionTrackList);
            NamesPlaylist.add(t);
            t = new Playlist("Избранные", loveTrackCollection);
            NamesPlaylist.add(t);
            PlayListAdapter playAdatper = new  PlayListAdapter(this, NamesPlaylist);
            list_playlist.setAdapter(playAdatper);
            musicConnection = new ServiceConnection(){
                @Override
                public void onServiceConnected(ComponentName name, IBinder service) {
                    MusicBinder binder = (MusicBinder)service;
                    //get service
                    musicSrv = binder.getService();
                    //pass list
                    musicSrv.setList(ActiveTrackList);
                    musicBound = true;
                    Log.d(LOG_TAG, "musicBound Запущен");

                }

                @Override
                public void onServiceDisconnected(ComponentName name) {
                    musicBound = false;
                }
            };
        }
        catch (Exception e){
            Log.d(LOG_TAG, "Ошибка в Крите: " + e.getMessage());
        }

    }
    private void Perem(){
        NavigationView navigationView = (NavigationView) findViewById(R.id.nav_view);
        //navigationView.setNavigationItemSelectedListener(this);
        titleBox = (TextView) findViewById(R.id.TitleTrack);
        LabelInfoArtist1= (TextView) findViewById(R.id.LabelInfoArtist1);
        LabelInfoArtist2= (TextView) findViewById(R.id.LabelInfoArtist2);
        LabelInfoArtist3= (TextView) findViewById(R.id.LabelInfoArtist3);
        LabelInfoArtist4= (TextView) findViewById(R.id.LabelInfoArtist4);
        LabelInfoTitle= (TextView) findViewById(R.id.LabelInfoTitle);
        LabelInfoJanr= (TextView) findViewById(R.id.LabelInfoJanr);
        LabelInfoPodJanr= (TextView) findViewById(R.id.LabelInfoPodJanr);
        LabelInfoLang= (TextView) findViewById(R.id.LabelInfoLang);
        LabelInfoNastr= (TextView) findViewById(R.id.LabelInfoNastr);
        LabelInfoDate= (TextView) findViewById(R.id.LabelInfoDate);
        editText=(EditText)findViewById(R.id.txtsearch);
        editText2 =(EditText)findViewById(R.id.txtsearch2);
        Classic = (CheckBox) findViewById(R.id.checkBoxClassic);
        Meloman =  (CheckBox) findViewById(R.id.checkBoxMeloman);
        playBtn = (Button) findViewById(R.id.playBtn);
        elapsedTimeLabel = (TextView) findViewById(R.id.elapsedTimeLabel);
        remainingTimeLabel = (TextView) findViewById(R.id.remainingTimeLabel);
        Nav_trackBTN = (Button) findViewById(R.id.nav_track);
        repeat = (Button) findViewById(R.id.repeatBtn);
        pustishka = (Button) findViewById(R.id.btn0);
        Create_track_list = (LinearLayout) findViewById(R.id.Layout_track);
        Player_list = (LinearLayout) findViewById(R.id.Player);
        AllTrackListView = (ListView) findViewById(R.id.list_track);
        ActiveTrackListView = (ListView) findViewById(R.id.active_list_track);
        list_playlist = (ListView) findViewById(R.id.list_playlist);
        Active_layout_track = (LinearLayout) findViewById(R.id.Active_Layout_track);
        Create_New_track_list = (LinearLayout) findViewById(R.id.Menu_Create_track);
        Create_New_track_list_artist = (LinearLayout) findViewById(R.id.Menu_Create_track_Artist);
        Create_New_track_list_name = (LinearLayout) findViewById(R.id.Menu_Create_track_Name);
        Create_New_track_list_janr = (LinearLayout) findViewById(R.id.Menu_Create_track_Janr);
        Create_New_track_list_podjanr = (LinearLayout) findViewById(R.id.Menu_Create_track_PodJanr);
        Create_New_track_list_lang = (LinearLayout) findViewById(R.id.Menu_Create_track_Lang);
        Create_New_track_list_nastr = (LinearLayout) findViewById(R.id.Menu_Create_track_Nastr);
        Create_Playlist_menu = (LinearLayout) findViewById(R.id.Create_Playlist_menu);
        Create_Playlist_menu_teg_type = (LinearLayout) findViewById(R.id.Create_Playlist_menu_teg_type);
        Create_Playlist_menu_teg_value = (LinearLayout) findViewById(R.id.Create_Playlist_menu_teg_value);
        Create_Playlist_menu_classic_name = (LinearLayout) findViewById(R.id.Create_Playlist_menu_classic_name);
        Nastroyki = (LinearLayout) findViewById(R.id.Menu_nastroyki);
        AllTrackList = new ArrayList<>();
        ActiveTrackList = new ArrayList<>();
        PlayTrackList = new ArrayList<>();
        CollectionTrackList = new ArrayList<>();
        navigationView = (NavigationView) findViewById(R.id.nav_view);
        dbHelper = new DBHelper(this);
        redact_track = new Track(-1,".",".");
        //Текстовые поля при добавлении песни
        artist_box = (TextView) findViewById(R.id.Artist_tb); //Метка АРТИСТ
        artist_box1 = (TextView) findViewById(R.id.Artist_tb2); //Метка АРТИСТ2
        artist_box2 = (TextView) findViewById(R.id.Artist_tb3); //Метка АРТИСТ3
        artist_box3 = (TextView) findViewById(R.id.Artist_tb4); //Метка АРТИСТ4
        artist_box4 = (TextView) findViewById(R.id.Artist_tb5); //Метка АРТИСТ5
        artist_box5 = (TextView) findViewById(R.id.Artist_tb6); //Метка АРТИСТ6
        artist_box.post(new Runnable() {
            @Override
            public void run() {
                InputMethodManager inputMethodManager =
                        (InputMethodManager)getSystemService(INPUT_METHOD_SERVICE);
                inputMethodManager.toggleSoftInputFromWindow(
                        artist_box.getApplicationWindowToken(),InputMethodManager.SHOW_IMPLICIT, 0);
                artist_box.requestFocus();
            }
        });
        name_box = (TextView) findViewById(R.id.Name_tb); //Метка НАЗВАНИЕ
        name_box.post(new Runnable() {
            @Override
            public void run() {
                InputMethodManager inputMethodManager =
                        (InputMethodManager)getSystemService(INPUT_METHOD_SERVICE);
                inputMethodManager.toggleSoftInputFromWindow(
                        name_box.getApplicationWindowToken(),InputMethodManager.SHOW_IMPLICIT, 0);
                name_box.requestFocus();
            }
        });
        Janr_sp = (Spinner) findViewById(R.id.Janr_sp);
        Janr_sp.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {
            @Override
            public void onItemSelected(AdapterView<?> parent, View view,
                                       int position, long id) {
                // показываем позиция нажатого элемента
                try {
                    Janr = arraySpinner[position];
                    Log.d(LOG_TAG,arraySpinner[position]);
                    //    Toast.makeText(getBaseContext(), "Position = " + position, Toast.LENGTH_SHORT).show();
                    podjanr_box.setAdapter(adapter(Podjanr_value(Janr)));
                    podjanr_box.setPrompt("Поджанр");
                }catch (Exception e){

                }

            }
            @Override
            public void onNothingSelected(AdapterView<?> arg0) {
            }
        });
        podjanr_box = (Spinner) findViewById(R.id.PodJanr_tb);//Метка ПОДЖАНР
        podjanr_box.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {
            @Override
            public void onItemSelected(AdapterView<?> parent, View view,
                                       int position, long id) {
                // показываем позиция нажатого элемента
                try {
                    PodJanr = arraySpinner_podjanr[position];
                    Log.d(LOG_TAG, arraySpinner_podjanr[position]);
                    arraySpinner_lang[0] = "Русский язык";
                    arraySpinner_lang[1] = "Английский язык";
                    arraySpinner_lang[2] = "Немецкий язык";
                    arraySpinner_lang[3] = "Португальский язык";
                    arraySpinner_lang[4] = "Французский язык";
                    arraySpinner_lang[5] = "Испанский язык";
                    arraySpinner_lang[6] = "Арабский язык";
                    arraySpinner_lang[7] = "Китайский язык";
                    lang_box.setAdapter(adapter(arraySpinner_lang));
                    lang_box.setPrompt("Язык вокала");
                }
                catch (Exception e){
                    Log.d(LOG_TAG, "Творится дичь");
                }
            }
            @Override
            public void onNothingSelected(AdapterView<?> arg0) {
                try{

                }catch (Exception e){}
            }
        });
        lang_box = (Spinner) findViewById(R.id.Metka_Lang_tb); //Метка ЯЗЫК ВАКАЛА
        lang_box.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {
            @Override
            public void onItemSelected(AdapterView<?> parent, View view,
                                       int position, long id) {
                // показываем позиция нажатого элемента
                try {
                    Lang_ = arraySpinner_lang[position];
                    Log.d(LOG_TAG, arraySpinner_lang[position]);
                }
                catch (Exception e){

                }
            }
            @Override
            public void onNothingSelected(AdapterView<?> arg0) {
                try{

                }catch (Exception e){}
            }
        });
        nastr_box = (Spinner) findViewById(R.id.Metka_Nastr_tb); //Метка НАСТРОЕНИЕ
        nastr_box.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {
            @Override
            public void onItemSelected(AdapterView<?> parent, View view,
                                       int position, long id) {
                // показываем позиция нажатого элемента
                try {
                    Nastr = arrayNastroenie_list[position];
                    Log.d(LOG_TAG, arrayNastroenie_list[position]);
                }
                catch (Exception e){

                }
            }
            @Override
            public void onNothingSelected(AdapterView<?> arg0) {
                try{

                }catch (Exception e){}
            }
        });
        Sp_teg_type = (Spinner) findViewById(R.id.Sp_teg_type);
        Sp_teg_type.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {
            @Override
            public void onItemSelected(AdapterView<?> parent, View view,
                                       int position, long id) {
                // показываем позиция нажатого элемента
                try {
                    ArrayList<String> tatcher = new  ArrayList<String>();
                    Map<String, Integer> letters= new HashMap<String, Integer>();
                    //arraySpinnerTeg_2 = new String[CollectionTrackList.size()];
                    // Инициализирую счетчик
                    int i;
                    Track_Teg = position;
                    switch (position) {
                        case 0:
                            Log.d(LOG_TAG, "Было выбрано по Исполнителю");
                            tatcher = new  ArrayList<String>();
                            //arraySpinnerTeg_2 = new String[CollectionTrackList.size()];
                            i = 0;
                            for (Track wrd : CollectionTrackList) {
                                if (!letters.containsKey(wrd.getArtist())) {
                                    letters.put(wrd.getArtist(), 1);
                                } else {
                                    letters.put(wrd.getArtist(), letters.get(wrd.getArtist()) + 1);
                                }
                                if (!letters.containsKey(wrd.getArtist2())) {
                                    letters.put(wrd.getArtist2(), 1);
                                } else {
                                    letters.put(wrd.getArtist2(), letters.get(wrd.getArtist2()) + 1);
                                }
                                if (!letters.containsKey(wrd.getArtist3())) {
                                    letters.put(wrd.getArtist3(), 1);
                                } else {
                                    letters.put(wrd.getArtist3(), letters.get(wrd.getArtist3()) + 1);
                                }
                                if (!letters.containsKey(wrd.getArtist4())) {
                                    letters.put(wrd.getArtist4(), 1);
                                } else {
                                    letters.put(wrd.getArtist4(), letters.get(wrd.getArtist4()) + 1);
                                }
                                if (!letters.containsKey(wrd.getArtist5())) {
                                    letters.put(wrd.getArtist5(), 1);
                                } else {
                                    letters.put(wrd.getArtist5(), letters.get(wrd.getArtist5()) + 1);
                                }
                                if (!letters.containsKey(wrd.getArtist6())) {
                                    letters.put(wrd.getArtist6(), 1);
                                } else {
                                    letters.put(wrd.getArtist6(), letters.get(wrd.getArtist6()) + 1);
                                }
                            }
                            for (Map.Entry<String, Integer> entry : letters.entrySet()) {
                                tatcher.add(entry.getKey());
                                i++;
                                //System.out.println("Буква = " + entry.getKey() + ", Повторений = " + entry.getValue());
                            }

                            break;
                        case 1:
                            Log.d(LOG_TAG, "Было выбрано по жанрам");
                            tatcher =  new  ArrayList<String>();
                            //arraySpinnerTeg_2 = new String[CollectionTrackList.size()];
                            i = 0;
                            for (Track wrd : CollectionTrackList) {
                                if (!letters.containsKey(wrd.getJanr())) {
                                    letters.put(wrd.getJanr(), 1);
                                } else {
                                    letters.put(wrd.getJanr(), letters.get(wrd.getJanr()) + 1);
                                }
                            }
                            for (Map.Entry<String, Integer> entry : letters.entrySet()) {
                                tatcher.add(entry.getKey());
                                i++;
                                //System.out.println("Буква = " + entry.getKey() + ", Повторений = " + entry.getValue());
                            }

                            Log.d(LOG_TAG, ""+tatcher.size());


                            break;
                        case 2:
                            Log.d(LOG_TAG, "Было выбрано по поджанрам");
                            tatcher = new  ArrayList<String>();
                            //arraySpinnerTeg_2 = new String[CollectionTrackList.size()];
                            i = 0;
                            for (Track wrd : CollectionTrackList) {
                                Log.d(LOG_TAG, "поджанр - " + wrd.getPodJanr());
                                if (!letters.containsKey(wrd.getPodJanr())) {
                                    letters.put(wrd.getPodJanr(), 1);
                                } else {
                                    letters.put(wrd.getPodJanr(), letters.get(wrd.getPodJanr()) + 1);
                                }
                            }
                            for (Map.Entry<String, Integer> entry : letters.entrySet()) {
                                tatcher.add(entry.getKey());
                                i++;
                                //System.out.println("Буква = " + entry.getKey() + ", Повторений = " + entry.getValue());
                            }



                            break;
                        case 3:
                            Log.d(LOG_TAG, "Было выбрано по Вокалу");
                            tatcher = new  ArrayList<String>();
                            //arraySpinnerTeg_2 = new String[CollectionTrackList.size()];
                            i = 0;
                            for (Track wrd : CollectionTrackList) {
                                if (!letters.containsKey(wrd.getMetka1())) {
                                    letters.put(wrd.getMetka1(), 1);
                                } else {
                                    letters.put(wrd.getMetka1(), letters.get(wrd.getMetka1()) + 1);
                                }
                            }
                            for (Map.Entry<String, Integer> entry : letters.entrySet()) {
                                tatcher.add(entry.getKey());
                                i++;
                                //System.out.println("Буква = " + entry.getKey() + ", Повторений = " + entry.getValue());
                            }


                            break;
                        case 4:
                            Log.d(LOG_TAG, "Было выбрано по Настроению");
                            tatcher = new  ArrayList<String>();
                            //arraySpinnerTeg_2 = new String[CollectionTrackList.size()];
                            i = 0;
                            for (Track wrd : CollectionTrackList) {
                                if (!letters.containsKey(wrd.getMetka2())) {
                                    letters.put(wrd.getMetka2(), 1);
                                } else {
                                    letters.put(wrd.getMetka2(), letters.get(wrd.getMetka2()) + 1);
                                }
                            }
                            for (Map.Entry<String, Integer> entry : letters.entrySet()) {
                                tatcher.add(entry.getKey());
                                i++;
                                //System.out.println("Буква = " + entry.getKey() + ", Повторений = " + entry.getValue());
                            }
                            Log.d(LOG_TAG, arraySpinnerTeg_2[0] + arraySpinnerTeg_2[1]);

                            break;
                    }

                    arraySpinnerTeg_2 = new String[tatcher.size()];
                    for (int ii = 0; ii < tatcher.size(); ii++) {
                        if(tatcher.get(ii) != "" && tatcher.get(ii) !=".")
                            arraySpinnerTeg_2[ii] = tatcher.get(ii);
                    }
                    Sp_teg_value.setAdapter(adapter(arraySpinnerTeg_2));
                    Sp_teg_value.setPrompt("Значение метки");
                    //Nastr = arraySpiner_create_list[position];
                    Log.d(LOG_TAG, arraySpiner_create_list[position]);
                    Log.d(LOG_TAG, "Размер массива: "+arraySpinnerTeg_2.length);
                }
                catch (Exception e){
                    Log.d(LOG_TAG, e.getMessage());
                }
            }
            @Override
            public void onNothingSelected(AdapterView<?> arg0) {
                try{

                }catch (Exception e){}
            }
        });
        Sp_teg_value = (Spinner) findViewById(R.id.Sp_teg_value); //Метка НАСТРОЕНИЕ
        Sp_teg_value.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {
            @Override
            public void onItemSelected(AdapterView<?> parent, View view,
                                       int position, long id) {
                // показываем позиция нажатого элемента
                try {
                    types_playlist_teg = arraySpinnerTeg_2[position];

                }
                catch (Exception e){

                }
            }
            @Override
            public void onNothingSelected(AdapterView<?> arg0) {
                try{

                }catch (Exception e){}
            }
        });
        Sp_sort_value = (Spinner) findViewById(R.id.Sp_sort_value);
        Sp_sort_value.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {
            @Override
            public void onItemSelected(AdapterView<?> parent, View view,
                                       int position, long id) {
                // показываем позиция нажатого элемента
                try {
                    switch(position) {
                        case 0:
                            Collections.sort(ActiveTrackList, new Comparator<Track>() {
                                public int compare(Track a, Track b) {
                                    return a.getArtist().compareTo(b.getArtist()); }
                            });
                            break;
                        case 1:
                            Collections.sort(ActiveTrackList, new Comparator<Track>() {
                                public int compare(Track a, Track b) {
                                    return a.getTitle().compareTo(b.getTitle()); }
                            });
                            break;
                        case 2:
                            Collections.sort(ActiveTrackList, new Comparator<Track>() {
                                public int compare(Track a, Track b) {
                                    return a.getDate().compareTo(b.getDate()); }
                            });

                            break;

                    }
                    Reset_active_list();
                    ////ВОТ ТУТ НАДО ПОДУМАТЬ НАД КОНТЕКСТОМ И this в АДАПТЕРЕ!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                    //SongAdapterActiveList songActAdt = new SongAdapterActiveList(ActiveTrackList);
                    //ActiveTrackListView.setAdapter(songActAdt);
                    //Log.d(LOG_TAG, arrayNastroenie_list[position]);
                }
                catch (Exception e){

                }
            }
            @Override
            public void onNothingSelected(AdapterView<?> arg0) {
                try{

                }catch (Exception e){}
            }
        });
        Language = new ArrayList<Lang>();
        Language.add(new Lang("001","Русский язык"));
        Language.add(new Lang("002","Английский язык"));
        Language.add(new Lang("003","Немецкий язык"));
        Language.add(new Lang("004","Португальский язык"));
        Language.add(new Lang("005","Французский язык"));
        Language.add(new Lang("006","Испанский язык"));
        Language.add(new Lang("007","Арабский язык"));
        Language.add(new Lang("008","Китайский язык"));
        loveBtn = (Button) findViewById(R.id.loveBtn);
        Janr_list = new ArrayList<Lang>();
        Janr_list.add(new Lang("001", "Народная музыка"));
        Janr_list.add(new Lang("002", "Духовная музыка"));
        Janr_list.add(new Lang("003", "Классическая музыка"));
        Janr_list.add(new Lang("004", "Фолк-музыка"));
        Janr_list.add(new Lang("005", "Кантри"));
        Janr_list.add(new Lang("006", "Латиноамериканская музыка"));
        Janr_list.add(new Lang("007", "Блюз"));
        Janr_list.add(new Lang("008", "Ритм-н-блюз"));
        Janr_list.add(new Lang("009", "Джаз"));
        Janr_list.add(new Lang("010", "Электронная музыка"));
        Janr_list.add(new Lang("011", "Рок"));
        Janr_list.add(new Lang("012", "Шансон"));
        Janr_list.add(new Lang("013", "Романс"));
        Janr_list.add(new Lang("014", "Авторская песня"));
        Janr_list.add(new Lang("015", "Поп"));
        Janr_list.add(new Lang("016", "Хип-хоп"));
        int i = 0;
        for(Lang l : Janr_list){
            arraySpinner[i] = l.getNames();
            i++;
        }
        Sp_sort_value.setAdapter(adapter(arraySpiner_sort_collection));
        Sp_sort_value.setPrompt("Сортировка");
        Sp_teg_type.setAdapter(adapter(arraySpiner_create_list));
        Sp_teg_type.setPrompt("Тип метки");
        Janr_sp.setAdapter(adapter(arraySpinner));
        Janr_sp.setPrompt("Жанр");
        nastr_box.setAdapter(adapter(arrayNastroenie_list));
        nastr_box.setPrompt("Вызывает эмоции");
        editText.addTextChangedListener(new TextWatcher() {

            @Override
            public void beforeTextChanged(CharSequence s, int start, int count, int after) {
            }

            @Override
            public void onTextChanged(CharSequence s, int start, int before, int count) {
                try {
                    if(s.toString().equals("")){
                        // reset listview
                        initList(1);
                    } else {
                        // perform search
                        searchItem(s.toString(),1);
                    }
                }catch (Exception e){
                    Log.d(LOG_TAG, "Ошибка в ЭДИТЕ поиска: " + e.getMessage());
                }

            }
            @Override
            public void afterTextChanged(Editable s) {
            }

        });
        editText2.addTextChangedListener(new TextWatcher() {

            @Override
            public void beforeTextChanged(CharSequence s, int start, int count, int after) {
            }

            @Override
            public void onTextChanged(CharSequence s, int start, int before, int count) {
                try {
                    if(s.toString().equals("")){
                        // reset listview
                        initList(2);
                    } else {
                        // perform search
                        searchItem(s.toString(),2);
                    }
                }catch (Exception e){
                    Log.d(LOG_TAG, "Ошибка в ЭДИТЕ поиска: " + e.getMessage());
                }

            }
            @Override
            public void afterTextChanged(Editable s) {
            }

        });
        //registerForContextMenu(ActiveTrackListView);
    }
    public void CloseActiveLayotClick(View view){
        Active_layout_track.setVisibility(LinearLayout.GONE);
        Player_list.setVisibility(LinearLayout.VISIBLE);
        ActiveTrackList = new ArrayList<Track>();
        ActiveTrackList = Clone;
        SongAdapterActiveList songActAdt = new SongAdapterActiveList(this, ActiveTrackList);
        ActiveTrackListView.setAdapter(songActAdt);
    }//закрытие активного плейлиста
    @Override
    protected void onStart() {
        super.onStart();
        if(playIntent==null){
            playIntent = new Intent(this, MusicService.class);
            bindService(playIntent, musicConnection, Context.BIND_AUTO_CREATE);
            startService(playIntent);
        }
    }
    public void CreatePlaylist(View view){
        try {
            Playlist p = NamesPlaylist.get(Integer.parseInt(view.getTag().toString()));
            ActiveTrackList = new ArrayList<Track>();
            for (Track T : p.getSongs()) {
                ActiveTrackList.add(T);
            }
            musicSrv.setList(ActiveTrackList);
            SongAdapterActiveList songActAdt = new SongAdapterActiveList(this, ActiveTrackList);
            ActiveTrackListView.setAdapter(songActAdt);
            Log.d(LOG_TAG, "Готово, выбор прошел успешно");
            Playlists.setVisibility(LinearLayout.GONE);
            Player_list.setVisibility(LinearLayout.VISIBLE);
            //ActiveTrackList = p.getSongs();
        }catch (Exception e){
            Log.d(LOG_TAG, e.getMessage());
        }
    } //Выбор плейлиста
    public void Next_create_song(View view) throws IOException{
        switch(view.getId()) {
            case R.id.Add_song_next1: // идентификатор "@+id/Add_song_next1"
                Create_New_track_list_artist.setVisibility(LinearLayout.GONE);
                Create_New_track_list_name.setVisibility(LinearLayout.VISIBLE);
                break;
            case R.id.Add_song_next2: // идентификатор "@+id/Add_song_next2"
                Create_New_track_list_name.setVisibility(LinearLayout.GONE);
                Create_New_track_list_janr.setVisibility(LinearLayout.VISIBLE);
                break;
            case R.id.Add_song_next3: // идентификатор "@+id/Add_song_next3"
                Create_New_track_list_janr.setVisibility(LinearLayout.GONE);
                Create_New_track_list_podjanr.setVisibility(LinearLayout.VISIBLE);
                break;
            case R.id.Add_song_next4: // идентификатор "@+id/Add_song_next4"
                Create_New_track_list_podjanr.setVisibility(LinearLayout.GONE);
                Create_New_track_list_lang.setVisibility(LinearLayout.VISIBLE);
                break;
            case R.id.Add_song_next5: // идентификатор "@+id/Add_song_next5"
                Create_New_track_list_lang.setVisibility(LinearLayout.GONE);
                Create_New_track_list_nastr.setVisibility(LinearLayout.VISIBLE);
                break;
            case R.id.NextBtn:
                musicSrv.playNext();
                players();
                break;
            case R.id.playBtn:
                if(!musicSrv.isPng()){
                    //Stoping
                    if(pauseSong == 0) {
                        pauseSong = 1;
                        //Log.d(LOG_TAG, "Тут ошибка?");
                        musicSrv.playSong();
                        //Log.d(LOG_TAG, "Тут ошибка???");
                    } else {
                        if(pauseSong == 2) {
                            musicSrv.go();
                        }
                    }
                    playBtn.setBackgroundResource(R.drawable.stop);
                    players();

                }
                else {
                    //Starting
                    musicSrv.pauseSong();
                    pauseSong = 2;
                    playBtn.setBackgroundResource(R.drawable.play);
                }
                break;
            case R.id.previousBtn:
                musicSrv.playPrev();
                players();
                break;
            case R.id.Red_track: //Кнопка редактирования трека
                Into_Track_layout.setVisibility(LinearLayout.GONE);
                Create_New_track_list.setVisibility(LinearLayout.VISIBLE);
                Create_track_list.setVisibility(LinearLayout.GONE);
                Create_New_track_list_artist.setVisibility(LinearLayout.VISIBLE);
                artist_box.setText(redact_track.getArtist());
                name_box.setText(redact_track.getTitle());
                Clone = new ArrayList<Track>();
                for(Track t:AllTrackList){
                    Clone.add(t);
                }
                redacter = 2;
                break;
            case R.id.Close_info: //Закрыть информацию о треке
                Into_Track_layout.setVisibility(View.GONE);
                Active_layout_track.setVisibility(View.VISIBLE);
                break;
            case R.id.loveBtn: //Кнопка Лайк
                Log.d(LOG_TAG, "Сохранение в избранные");
                Log.d(LOG_TAG, "Название " +titleBox.getText() );
                if(love == false)
                {
                for(Track t: ActiveTrackList) {
                    //Log.d(LOG_TAG, "Название " +t.getTitle());
                    if (t.getTitle() == titleBox.getText()) {
                        //loveTrackCollection.add(t);
                        newBaseTrack(t, "mytable_1");
                        loveBtn.setBackgroundResource(R.drawable.btn_like_active);
                        loveTrackCollection.add(t);
                    }
                }
                }else {
                    loveBtn.setBackgroundResource(R.drawable.btn_like);
                    for (Track t : ActiveTrackList) {
                        //Log.d(LOG_TAG, "Название " +t.getTitle());
                        if (t.getTitle() == titleBox.getText()) {
                            //loveTrackCollection.add(t);
                            delTrackBase(t, "mytable_1");

                        }
                    }
                }

                break;
            case R.id.repeatBtn: //Кнопка повтора
                if(repeat_ == 0){
                    repeat_ = 1;
                    repeat.setBackgroundResource(R.drawable.repeat_one_btn);
                    musicSrv.loop(true);
                }else
                if(repeat_ == 1){
                    repeat_ = 2;
                    repeat.setBackgroundResource(R.drawable.random_btn);
                    musicSrv.loop(false);
                    musicSrv.setShuffle(true);
                } else
                if(repeat_ == 2){
                    repeat_ = 0;
                    repeat.setBackgroundResource(R.drawable.repeat_btn);
                    musicSrv.setShuffle(false);
                }
                break;
            case R.id.listBtn:   //Открытие активного плейлиста
                Player_list.setVisibility(LinearLayout.GONE);
                Active_layout_track.setVisibility(LinearLayout.VISIBLE);
                Clone = new ArrayList<Track>();
                for(Track t:ActiveTrackList){
                    Clone.add(t);
                }
                break;
            case R.id.Create_Playlist:
                Playlists.setVisibility(LinearLayout.GONE);
                Create_Playlist_menu.setVisibility(LinearLayout.VISIBLE);
                break;
            case R.id.Create_playlist_menu_close:
                Playlists.setVisibility(LinearLayout.VISIBLE);
                Create_Playlist_menu.setVisibility(LinearLayout.GONE);
                break;
            case R.id.Create_playlist_menu_teg_type_close:
                Playlists.setVisibility(LinearLayout.VISIBLE);
                Create_Playlist_menu_teg_type.setVisibility(LinearLayout.GONE);
                break;
            case R.id.Create_playlist_menu_teg_value_close:
                Playlists.setVisibility(LinearLayout.VISIBLE);
                Create_Playlist_menu_teg_value.setVisibility(LinearLayout.GONE);
                break;
            case R.id.Create_playlist_menu_classic_name_close:
                Playlists.setVisibility(LinearLayout.VISIBLE);
                Create_Playlist_menu_classic_name.setVisibility(LinearLayout.GONE);
                break;
            case R.id.Create_playlist_menu_teg:
                Create_Playlist_menu.setVisibility(LinearLayout.GONE);
                Create_Playlist_menu_teg_type.setVisibility(LinearLayout.VISIBLE);
                break;
            case R.id.Create_playlist_menu_teg_type_next:
                Create_Playlist_menu_teg_type.setVisibility(LinearLayout.GONE);
                Create_Playlist_menu_teg_value.setVisibility(LinearLayout.VISIBLE);
                break;
            case R.id.Create_playlist_menu_clas:
                Create_Playlist_menu.setVisibility(LinearLayout.GONE);
                Create_Playlist_menu_classic_name.setVisibility(LinearLayout.VISIBLE);
                break;
            case R.id.Create_playlist_menu_teg_value_create:
                try {
                    if(Playlist_key == 3)
                        return;
                    ListCollectionTeg =  new ArrayList<Track>();
                    switch (Track_Teg) {
                        case 0:
                            for(Playlist t: NamesPlaylist){
                                if(t.getZvan() == types_playlist_teg){
                                    NamesPlaylist.remove(t);
                                    break;
                                }
                            }
                            for (Track t : CollectionTrackList) {
                                if (t.getArtist().equals(types_playlist_teg)  || t.getArtist2().equals(types_playlist_teg)  || t.getArtist3().equals(types_playlist_teg)  || t.getArtist4().equals(types_playlist_teg)  || t.getArtist5().equals(types_playlist_teg)  || t.getArtist6().equals(types_playlist_teg)) {
                                    ListCollectionTeg.add(t);
                                }
                            }
                            Playlist tt = new Playlist("По исполнителю: "+types_playlist_teg, ListCollectionTeg);
                            NamesPlaylist.add(tt);
                            Playlist_key++;
                            break;
                        case 1:
                            for (Track t : CollectionTrackList) {
                                if (t.getJanr().equals(types_playlist_teg) ) {
                                    ListCollectionTeg.add(t);
                                }
                            }
                            tt = new Playlist("По Жанру: "+types_playlist_teg, ListCollectionTeg);
                            NamesPlaylist.add(tt);Playlist_key++;
                            break;
                        case 2:
                            for (Track t : CollectionTrackList) {
                                if (t.getPodJanr().equals(types_playlist_teg) ) {
                                    ListCollectionTeg.add(t);
                                }
                            }
                            tt = new Playlist("По поджанру: "+types_playlist_teg, ListCollectionTeg);
                            tt.setZvan(types_playlist_teg);
                            NamesPlaylist.add(tt);Playlist_key++;
                            break;
                        case 3:
                            for (Track t : CollectionTrackList) {
                                if (t.getMetka1().equals(types_playlist_teg) ) {
                                    ListCollectionTeg.add(t);
                                }
                            }
                            tt = new Playlist("По языку вокала: "+types_playlist_teg, ListCollectionTeg);
                            NamesPlaylist.add(tt);Playlist_key++;
                            break;
                        case 4:
                            for (Track t : CollectionTrackList) {
                                if (t.getMetka2().equals(types_playlist_teg) ) {
                                    ListCollectionTeg.add(t);
                                }
                            }
                            tt = new Playlist("По вызываемому настроению: "+types_playlist_teg, ListCollectionTeg);
                            NamesPlaylist.add(tt);Playlist_key++;
                            break;
                    }

                }catch (Exception e){
                    Log.d(LOG_TAG, e.getMessage());
                }
                Create_Playlist_menu_teg_value.setVisibility(LinearLayout.GONE);
                Playlists.setVisibility(LinearLayout.VISIBLE);
                PlayListAdapter playAdatper = new  PlayListAdapter(this, NamesPlaylist);
                list_playlist.setAdapter(playAdatper);
                break;
            case R.id.addButton:
                try {
                    active_track_int = Integer.parseInt(view.getTag().toString());
                    Log.d(LOG_TAG, "Прикинь, я нажал на "+ view.getTag().toString());
                    final CharSequence[] items = {"Добавить в плейлист", "Информация о треке", "Удалить"};

                    AlertDialog.Builder builder = new AlertDialog.Builder(this);
                    builder.setTitle("Выбор действий");
                    builder.setItems(items, new DialogInterface.OnClickListener() {
                        public void onClick(DialogInterface dialog, int item) {
                            if(item == 0){

                            }else if(item == 1){
                                InfoTrack(ActiveTrackList.get(active_track_int));
                                Into_Track_layout.setVisibility(View.VISIBLE);
                                Active_layout_track.setVisibility(View.GONE);
                            }
                            else if (item == 2){

                            }

                            //Toast.makeText(getApplicationContext(), items[item], Toast.LENGTH_SHORT).show();
                        }
                    });
                    AlertDialog alert = builder.create();
                    alert.show();
                    //openContextMenu(ActiveTrackListView);
                }catch (Exception e){
                    Log.d(LOG_TAG, "Ошибка в триточки "+e.getMessage());
                }
                break;
        }
    } //Нажатия кнопок
    public void songPicked(View view) throws IOException {
        musicSrv.setSong(Integer.parseInt(view.getTag().toString()));
        Log.d(LOG_TAG,view.getTag().toString() );
        musicSrv.playSong();
        playBtn.setBackgroundResource(R.drawable.stop);
        players();

    } //Запуск песни
    public void NextCreateSong2(View v){

        if(redacter == 1){
            SimpleDateFormat sdf = new SimpleDateFormat("dd/MM/yyyy");
            String string_date = sdf.format(Calendar.getInstance().getTime());
            Date date = null;
            try {
                date = sdf.parse(string_date);
            } catch (ParseException e) {
                e.printStackTrace();
            }
            if(artist_box.getText().toString() != ""){
                redact_track.setArtist(artist_box.getText().toString());
            }
            TMP = new Track(CollectionTrackList.size()+1, Songer.getTitle(), Songer.getArtist());
            if(artist_box1.getText().toString() != ""){
                TMP.setArtist2(artist_box1.getText().toString());
            }
            if(artist_box2.getText().toString() != ""){
                TMP.setArtist3(artist_box2.getText().toString());
            }
            if(artist_box3.getText().toString() != ""){
                TMP.setArtist4(artist_box3.getText().toString());
            }
            if(artist_box4.getText().toString() != ""){
                TMP.setArtist5(artist_box4.getText().toString());
            }
            if(artist_box5.getText().toString() != ""){
                TMP.setArtist6(artist_box5.getText().toString());
            }
            if(name_box.getText().toString() != ""){
                TMP.setTitle(name_box.getText().toString());
            }
            Log.d(LOG_TAG,"Janr - "+ Janr);
            if(Janr != ""){
                TMP.setJanr(Janr);
            }
            else{
                TMP.setJanr(Janr_list.get(0).getNames());
            }
            if(PodJanr != ""){
                TMP.setPodJanr(PodJanr);
            }
            else{
                TMP.setPodJanr(Podjanr_list.get(0).getNames());
            }
            if(Lang_ != ""){
                TMP.setMetka1(Lang_);
            }
            else{
                TMP.setMetka1(Language.get(0).getNames());
            }
            if(Nastr!= ""){
                TMP.setMetka2(Nastr);
            }
            else{
                TMP.setMetka2(arrayNastroenie_list[0]);
            }
            //Toast.makeText(getApplicationContext(), Songer.getMetka7()+" "+Songer.getMetka8(), Toast.LENGTH_SHORT).show();
            TMP.setMetka7(Songer.getMetka7());
            TMP.setMetka8(Songer.getMetka8());
            TMP.setURL(uri_create);
            TMP.setDate(date);
            CollectionTrackList.add(TMP);
            newBaseTrack(TMP, "mytable");
            Create_New_track_list_nastr.setVisibility(LinearLayout.GONE);
            Create_New_track_list.setVisibility(LinearLayout.GONE);
            Create_track_list.setVisibility(LinearLayout.GONE);
            Player_list.setVisibility(LinearLayout.VISIBLE);
        }
        if(redacter == 2){
            SimpleDateFormat sdf = new SimpleDateFormat("dd/MM/yyyy");
            String string_date = sdf.format(Calendar.getInstance().getTime());
            Date date = null;
            try {
                date = sdf.parse(string_date);
            } catch (ParseException e) {
                e.printStackTrace();
            }
            if(artist_box.getText().toString() != ""){
                redact_track.setArtist(artist_box.getText().toString());
            }
            if(artist_box1.getText().toString() != ""){
                redact_track.setArtist2(artist_box1.getText().toString());
            }
            if(artist_box2.getText().toString() != ""){
                redact_track.setArtist3(artist_box2.getText().toString());
            }
            if(artist_box3.getText().toString() != ""){
                redact_track.setArtist4(artist_box3.getText().toString());
            }
            if(artist_box4.getText().toString() != ""){
                redact_track.setArtist5(artist_box4.getText().toString());
            }
            if(artist_box5.getText().toString() != ""){
                redact_track.setArtist6(artist_box5.getText().toString());
            }
            if(name_box.getText().toString() != ""){
                redact_track.setTitle(name_box.getText().toString());
            }
            Log.d(LOG_TAG,"Janr - "+ Janr);
            if(Janr != ""){
                redact_track.setJanr(Janr);
            }
            else{
                redact_track.setJanr(Janr_list.get(0).getNames());
            }
            if(PodJanr != ""){
                redact_track.setPodJanr(PodJanr);
            }
            else{
                redact_track.setPodJanr(Podjanr_list.get(0).getNames());
            }
            if(Lang_ != ""){
                redact_track.setMetka1(Lang_);
            }
            else{
                redact_track.setMetka1(Language.get(0).getNames());
            }
            if(Nastr!= ""){
                redact_track.setMetka2(Nastr);
            }
            else{
                redact_track.setMetka2(arrayNastroenie_list[0]);
            }
            boolean b = false;
            for(Track t: CollectionTrackList){
                if(t.getArtist() == redact_track.getArtist() || t.getTitle() == redact_track.getTitle()){
                    b = true;
                }
            }
            if(b){
                updBase(redact_track, "mytable");
            }
            else {
                redact_track.setDate(date);
                newBaseTrack(redact_track, "mytable");
            }

            Create_New_track_list_nastr.setVisibility(LinearLayout.GONE);
            Create_New_track_list.setVisibility(LinearLayout.GONE);
            Create_track_list.setVisibility(LinearLayout.GONE);
            Player_list.setVisibility(LinearLayout.VISIBLE);
        }
    } //Добавление трека в коллекцию
    public void CreateTrack(View view){
        try{
            Create_New_track_list.setVisibility(LinearLayout.VISIBLE);
            Create_track_list.setVisibility(LinearLayout.VISIBLE);
            Create_New_track_list_artist.setVisibility(LinearLayout.VISIBLE);
            //Поиск песни
            Songer = AllTrackList.get(Integer.parseInt(view.getTag().toString()));
            //Поиск по ID
            long currSong = Songer.getID();
            //Установка пути
            Uri trackUri = ContentUris.withAppendedId(
                    MediaStore.Audio.Media.EXTERNAL_CONTENT_URI,
                    currSong);

            //Toast.makeText(getApplicationContext(), newSong.getTitle(), Toast.LENGTH_SHORT).show();
            artist_box.setText(Songer.getArtist());
            name_box.setText(Songer.getTitle());
            Songer.setMetka7(Songer.getArtist());
            Songer.setMetka8(Songer.getTitle());
            uri_create = trackUri.toString();
            //Toast.makeText(getApplicationContext(), trackUri.toString(), Toast.LENGTH_SHORT).show();
            //Toast.makeText(getApplicationContext(), Adt.getName(), Toast.LENGTH_SHORT).show();
            //Toast.makeText(getApplicationContext(), mediaStorageDir.toString()+"/"+Adt.getName(), Toast.LENGTH_SHORT).show();
            return;
        }catch (Exception e){

            Log.d(LOG_TAG, "Ошибка при считывании полей");
        }
    } //Выбор песни для добавления в коллекцию
    public void Add_artist(View view){
        if(artist_box1.getVisibility()==View.GONE){
            artist_box1.setVisibility(View.VISIBLE);
        }else if(artist_box2.getVisibility()==View.GONE){
            artist_box2.setVisibility(View.VISIBLE);
        }else if(artist_box3.getVisibility()==View.GONE){
            artist_box3.setVisibility(View.VISIBLE);
        }else if(artist_box4.getVisibility()==View.GONE){
            artist_box4.setVisibility(View.VISIBLE);
        }else if(artist_box5.getVisibility()==View.GONE){
            artist_box5.setVisibility(View.VISIBLE);
        }

    } //Добавления артистов при добавлении песни в коллекцию
    public void getSongList() {

        ContentResolver contentResolver = getContentResolver();
        Uri uri = android.provider.MediaStore.Audio.Media.EXTERNAL_CONTENT_URI;
        //Toast.makeText(getApplicationContext(), uri.toString(), Toast.LENGTH_SHORT).show();
        Cursor musicCursor = contentResolver.query(uri, null, null, null, null);

            if(musicCursor!=null && musicCursor.moveToFirst()){
            int titleColumn =  musicCursor.getColumnIndex(MediaStore.Audio.Media.TITLE);
            int idColumn = musicCursor.getColumnIndex
                    (android.provider.MediaStore.Audio.Media._ID);
            int artistColumn = musicCursor.getColumnIndex(MediaStore.Audio.Media.ARTIST);
            do {
                long thisId = musicCursor.getLong(idColumn);
                String thisTitle = musicCursor.getString(titleColumn);
                String thisArtist = musicCursor.getString(artistColumn);
                AllTrackList.add(new Track(thisId, thisTitle, thisArtist));
            } while (musicCursor.moveToNext());
        }

    } //Сбор всех песен с памяти телефона
    private void SongList(){
        try {
            Iterator<Track> iter = CollectionTrackList.iterator();
            while (iter.hasNext()) {
                Track s = iter.next();
                Log.d(LOG_TAG, "При передаче номер песни в коллекции1: " + s.getMetka7());
                Log.d(LOG_TAG, "При передаче номер песни в коллекции1: " + s.getID());
                for(Track tt :AllTrackList){
                    if(s.getMetka7().equals(tt.getArtist()) == true) {
                        if (s.getMetka8().equals(tt.getTitle()) == true) {
                            s.setID(tt.getID());

                            break;
                        }
                    }
                }
                if(s.getID() == 0){
                    Log.d(LOG_TAG, "При передаче номер песни в коллекции2: " + s.getID());
                    delTrackBase(s, "mytable");
                    iter.remove();
                }
            }
        }catch (Exception e)
        {
            Log.d(LOG_TAG, "Ошибка в SongList: "+ e.getMessage());
        }
    } //Проверка на совпадения в коллекции и добавлении треков
    @Override
    public void onBackPressed() {

        new AlertDialog.Builder(this)
                //Здесь тупо кнопки залиты белым цветом
                .setTitle("Выйти из приложения?")
                .setMessage("Вы действительно хотите выйти?")
                .setNegativeButton(Html.fromHtml("<font color='#FF7F27'>Нет</font>"), new DialogInterface.OnClickListener() {

                    public void onClick(DialogInterface arg0, int arg1) {
                        return;
                    }
                })
                .setPositiveButton(Html.fromHtml("<font color='#FF7F27'>Да</font>"), new DialogInterface.OnClickListener() {
                    public void onClick(DialogInterface arg0, int arg1) {

                        MainActivity.super.onBackPressed();
                    }
                }).create().show();
    } //Выход из приложения
    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
        // Inflate the menu; this adds items to the action bar if it is present.
        getMenuInflater().inflate(R.menu.main, menu);
        return true;
    }
    @Override
    public boolean onOptionsItemSelected(MenuItem item) {
        // Handle action bar item clicks here. The action bar will
        // automatically handle clicks on the Home/Up button, so long
        // as you specify a parent activity in AndroidManifest.xml.
        int id = item.getItemId();

        //noinspection SimplifiableIfStatement
        if (id == R.id.action_settings) {
            return true;
        }
        //Случайный порядок
        //case R.id.action_shuffle:
        //musicSrv.setShuffle();
        //break;

        return super.onOptionsItemSelected(item);
    }
    @SuppressWarnings("StatementWithEmptyBody")
    public boolean onNavigationItemSelected(MenuItem item) {
        // Handle navigation view item clicks here.
        int id = item.getItemId();

        if (id == R.id.nav_track) {
            Player_list.setVisibility(LinearLayout.GONE);
            Create_track_list.setVisibility(LinearLayout.VISIBLE);
            Nastroyki.setVisibility(LinearLayout.GONE);
            Playlists.setVisibility(LinearLayout.GONE);
            Active_layout_track.setVisibility(View.GONE);
            Clone = new ArrayList<Track>();
            for(Track t:AllTrackList){
                Clone.add(t);
            }
            redacter = 1;
        } else if (id == R.id.nav_gallery) {
            Player_list.setVisibility(LinearLayout.VISIBLE);
            Create_track_list.setVisibility(LinearLayout.GONE);
            Nastroyki.setVisibility(LinearLayout.GONE);
            Playlists.setVisibility(LinearLayout.GONE);
            Active_layout_track.setVisibility(View.GONE);
            AllTrackList = new ArrayList<Track>();
            AllTrackList = Clone;
            SongAdapter songAllAdt = new SongAdapter(this, AllTrackList);
            AllTrackListView.setAdapter(songAllAdt);
            //controller.setVisibility(controller.GONE);
            //musicSrv.stop();
        //} else if (id == R.id.nav_collection) {
        //    ActiveTrackList.clear();
        //    ActiveTrackList = CollectionTrackList;

        } else if (id == R.id.nav_slideshow) {


        } else if (id == R.id.nav_playlist) {
            Player_list.setVisibility(LinearLayout.GONE);
            Create_track_list.setVisibility(LinearLayout.GONE);
            Nastroyki.setVisibility(LinearLayout.GONE);
            Playlists.setVisibility(LinearLayout.VISIBLE);
            Active_layout_track.setVisibility(View.GONE);
            AllTrackList = new ArrayList<Track>();
            AllTrackList = Clone;
            SongAdapter songAllAdt = new SongAdapter(this, AllTrackList);
            AllTrackListView.setAdapter(songAllAdt);
        } else if (id == R.id.nav_manage) {
            Nastroyki.setVisibility(LinearLayout.VISIBLE);
            Player_list.setVisibility(LinearLayout.GONE);
            Create_track_list.setVisibility(LinearLayout.GONE);
            Active_layout_track.setVisibility(View.GONE);
            Playlists.setVisibility(LinearLayout.GONE);
            AllTrackList = new ArrayList<Track>();
            AllTrackList = Clone;
            SongAdapter songAllAdt = new SongAdapter(this, AllTrackList);
            AllTrackListView.setAdapter(songAllAdt);
        } else if (id == R.id.nav_share) {

        }

        DrawerLayout drawer = (DrawerLayout) findViewById(R.id.drawer_layout);
        drawer.closeDrawer(GravityCompat.START);
        return true;
    } //Выбор навигации
    private Handler handler = new Handler(){
        @Override
        public void handleMessage(Message msg) {
            int currentPosition = msg.what;
            //Update Position Bar
            positionBar.setProgress(currentPosition);
            String elapsedTime = createTimeLabel(currentPosition);
            elapsedTimeLabel.setText(elapsedTime);
            String renautingTime = createTimeLabel(totalTime);
            remainingTimeLabel.setText(renautingTime);
        }
    };
    public String createTimeLabel(int time){
        String TimeLabel = "";
        int min = time / 1000 / 60;
        int sec = time / 1000 % 60;
        if(min==0 && sec==0) {
            titleBox.setText(musicSrv.SetTitle());
            for(Track tt : loveTrackCollection){
                    if(titleBox.getText() == tt.getTitle() ){
                        loveBtn.setBackgroundResource(R.drawable.btn_like_active);
                    }
            }

        }
        TimeLabel = min + ":";
        if(sec<10) TimeLabel += "0";
        TimeLabel += sec;
        return TimeLabel;
    }
    public  void players(){
        totalTime = musicSrv.setTime();
        //Toast toast = Toast.makeText(getApplicationContext(),
        //        musicSrv.SetTitle() , Toast.LENGTH_SHORT);
        //toast.show();
        //titleBox.setText(musicSrv.SetTitle());
        positionBar.setMax(musicSrv.setTime());
        positionBar.setOnSeekBarChangeListener(
                new SeekBar.OnSeekBarChangeListener() {
                    @Override
                    public void onProgressChanged(SeekBar seekBar, int progress, boolean fromUser) {
                        if (fromUser) {
                            musicSrv.seek(progress);
                            positionBar.setProgress(progress);
                        }
                    }

                    @Override
                    public void onStartTrackingTouch(SeekBar seekBar) {

                    }

                    @Override
                    public void onStopTrackingTouch(SeekBar seekBar) {

                    }
                }
        );
        new Thread(new Runnable() {
            @Override
            public void run() {
                while (musicSrv != null) {
                    try {
                        Message msg = new Message();
                        msg.what = musicSrv.getPosn();
                        handler.sendMessage(msg);
                        //Thread.sleep();
                    } catch (IndexOutOfBoundsException e) {
                        Toast toast = Toast.makeText(getApplicationContext(),
                                "Ошибочка в Thread", Toast.LENGTH_SHORT);
                        toast.show();
                    }
                }
            }
        }).start();
    }
    public void newChecked(View view){
        int id = view.getId();
        if(id == R.id.checkBoxClassic){
            Meloman.setChecked(false);
        }
        if(id == R.id.checkBoxMeloman){
            Classic.setChecked(false);
        }
    }
    @Override
    public void start() {
        musicSrv.go();
    }
    @Override
    public void pause() {
        playbackPaused=true;
        musicSrv.pausePlayer();
    }
    @Override
    public int getDuration() {
        if(musicSrv!=null && musicBound && musicSrv.isPng()) {
            totalTime = musicSrv.getDur();
            //Toast toast = Toast.makeText(getApplicationContext(),
            //        String.valueOf(totalTime), Toast.LENGTH_SHORT);
            //toast.show();
            return musicSrv.getDur();
        }
        else
        {
            totalTime = 0;
            return 0;
        }
    }
    @Override
    public int getCurrentPosition() {
        if(musicSrv!=null && musicBound && musicSrv.isPng())
        return musicSrv.getPosn();
        else return 0;
    }
    @Override
    public void seekTo(int pos) {
        musicSrv.seek(pos);
    }
    @Override
    public boolean isPlaying() {
        if(musicSrv!=null && musicBound)
        return musicSrv.isPng();
        return false;
    }
    @Override
    public int getBufferPercentage() {
        return 0;
    }
    @Override
    public boolean canPause() {
        return true;
    }
    @Override
    public boolean canSeekBackward() {
        return false;
    }
    @Override
    public boolean canSeekForward() {
        return false;
    }
    @Override
    public int getAudioSessionId() {
        return 0;
    }
    private void setController(){
        controller = new MusicController(this);
        //controller.setPrevNextListeners(new View.OnClickListener() {
        //    @Override
        //    public void onClick(View v) {
        //        playNext();
        //    }
        //}, new View.OnClickListener() {
        //    @Override
        //    public void onClick(View v) {
        //        playPrev();
        //    }
        //});
        controller.setMediaPlayer(this);
        controller.setAnchorView(findViewById(R.id.list_track));
        controller.setEnabled(true);
    }
    @Override
    protected void onPause(){
        super.onPause();
        paused=true;
    }
    @Override
    protected void onResume(){
        super.onResume();
        if(paused){
            setController();
            paused=false;
        }
    }
    @Override
    protected void onStop() {
        controller.hide();
        super.onStop();
    }
    @Override
    public void onCreateContextMenu(ContextMenu menu, View v,
                                    ContextMenu.ContextMenuInfo menuInfo) {

// TODO Auto-generated method stub
        switch (v.getId()) {
            case R.id.Player:
                super.onCreateContextMenu(menu, v, menuInfo);
                menu.add(0, 1, 0, "Коллекция");
                menu.add(0, 2, 0, "Все песни");
                menu.add(0, 3, 0, "Удалить песню");
                menu.add(0, 4, 0, "Редактировать теги");
                menu.add(0, 5, 0, "Сменить стиль");
                menu.add(0, 6, 0, "Пройти тест");
                break;
            //case R.id.tvSize:
            //    menu.add(0, MENU_SIZE_22, 0, "22");
            //    menu.add(0, MENU_SIZE_26, 0, "26");
            //    menu.add(0, MENU_SIZE_30, 0, "30");
            //    break;
        }
    }
    @Override
    public boolean onContextItemSelected(MenuItem item) {
        // TODO Auto-generated method stub
        AdapterView.AdapterContextMenuInfo info = (AdapterView.AdapterContextMenuInfo) item.getMenuInfo();
        switch (item.getItemId()) {
            case R.id.edit_track_menu:

                return true;
            case R.id.info_track_menu:

                return true;
            case R.id.create_track_menu:

                return true;
            default:
                return super.onContextItemSelected(item);
        }

}
    public void Reset_active_list(){
        SongAdapterActiveList songActAdt = new SongAdapterActiveList(this, ActiveTrackList);
        ActiveTrackListView.setAdapter(songActAdt);
    }

    public void searchItem(String textToSearch, int i){
        try {
            String textToSearch1 = textToSearch.toLowerCase();
            if(i == 1){

                Log.d(LOG_TAG, "1");
                Iterator<Track> iter = ActiveTrackList.iterator();
                while (iter.hasNext()) {
                    Track s = iter.next();
                    if(!s.getArtist().toLowerCase().contains(textToSearch1) & !s.getTitle().toLowerCase().contains(textToSearch1)){
                   // if(!s.getArtist().toLowerCase().contains(textToSearch1)){
                        Log.d(LOG_TAG, "2");
                        iter.remove();
                        Log.d(LOG_TAG, "3");
                    }
                }
                //ActiveTrackList = new ArrayList<Track>();
                //while (iter.hasNext()) {
                //    ActiveTrackList.add(iter.next());
                //}
                Log.d(LOG_TAG, "Размер клона: "+Clone.size());
                SongAdapterActiveList songActAdt = new SongAdapterActiveList(this, ActiveTrackList);
                Log.d(LOG_TAG, "5");
                ActiveTrackListView.setAdapter(songActAdt);
                //adapter.notifyDataSetChanged();
            }
            if( i == 2){
                Iterator<Track> iter = AllTrackList.iterator();
                while (iter.hasNext()) {
                    Track s = iter.next();
                    if(!s.getArtist().toLowerCase().contains(textToSearch1) & !s.getTitle().toLowerCase().contains(textToSearch1)){
                        // if(!s.getArtist().toLowerCase().contains(textToSearch1)){
                        Log.d(LOG_TAG, "2");
                        iter.remove();
                        Log.d(LOG_TAG, "3");
                    }
                }
                SongAdapter songAllAdt = new SongAdapter(this, AllTrackList);
                AllTrackListView.setAdapter(songAllAdt);
                Log.d(LOG_TAG, "Размер клона: "+Clone.size());
                //adapter.notifyDataSetChanged();
            }
        }catch (Exception e){
            Log.d(LOG_TAG, "Ошибка в поиске:" + e.getMessage());
        }
    } //Поиск в ListView
    public void initList(int i){
        try {
            if(i == 1){
                ActiveTrackList = new ArrayList<Track>();
                for (Track t:Clone){
                    ActiveTrackList.add(t);
                }
                Log.d(LOG_TAG, "Размер клона: "+Clone.size());
                SongAdapterActiveList songActAdt = new SongAdapterActiveList(this, ActiveTrackList);
                ActiveTrackListView.setAdapter(songActAdt);
            }
            if(i == 2 ){
                AllTrackList = new ArrayList<Track>();
                for (Track t:Clone){
                    AllTrackList.add(t);
                }
                Log.d(LOG_TAG, "Размер клона: "+Clone.size());
                SongAdapter songAllAdt = new SongAdapter(this, AllTrackList);
                AllTrackListView.setAdapter(songAllAdt);
            }
        }
       catch (Exception e){
            Log.d(LOG_TAG, "Ошибка в возврате коллекции: " + e.getMessage());
       }
    } //Инициализация после поиска в listView
    private String[] Podjanr_value(String Janr){
        if(Janr == "Народная музыка"){
            arraySpinner_podjanr = new String[6];
            arraySpinner_podjanr[0] = "Центральная Азия";
            arraySpinner_podjanr[1] = "Восточная и Южная Азия";
            arraySpinner_podjanr[2] = "Кавказ и Закавказье";
            arraySpinner_podjanr[3] = "Ближний Восток";
            arraySpinner_podjanr[4] = "Восточная Европа";
            arraySpinner_podjanr[5] = "Западная Европа";
        } else if(Janr == "Духовная музыка"){
            arraySpinner_podjanr = new String[5];
            arraySpinner_podjanr[0] = "Каббалистическая музыка";
            arraySpinner_podjanr[1] = "Апостольская музыка";
            arraySpinner_podjanr[2] = "Католическая музыка";
            arraySpinner_podjanr[3] = "Православная музыка";
            arraySpinner_podjanr[4] = "Афроамериканская духовная музыка";
        } else if(Janr == "Классическая музыка"){
            arraySpinner_podjanr = new String[15];
            arraySpinner_podjanr[0] = "Музыка карнатака";
            arraySpinner_podjanr[1] = "Арабская классическая музыка";
            arraySpinner_podjanr[2] = "Европейская классическая музыка";
            arraySpinner_podjanr[3] = "Средневековье";
            arraySpinner_podjanr[4] = "Возрождение";
            arraySpinner_podjanr[5] = "Барокко";
            arraySpinner_podjanr[6] = "Классицизм";
            arraySpinner_podjanr[7] = "Романтизм";
            arraySpinner_podjanr[8] = "Салонная музыка";
            arraySpinner_podjanr[9] = "Модернизм";
            arraySpinner_podjanr[10] = "Постмодернизм";
            arraySpinner_podjanr[11] = "Неоклассицизм";
            arraySpinner_podjanr[12] = "Электронная европейская классическая музыка";
            arraySpinner_podjanr[13] = "Оркестровая музыка";
            arraySpinner_podjanr[14] = "Вокальная, хоровая";
        } else if(Janr == "Фолк-музыка"){
            arraySpinner_podjanr = new String[4];
            arraySpinner_podjanr[0] = "Этническая музыка";
            arraySpinner_podjanr[1] = "Прогрессив-фолк";
            arraySpinner_podjanr[2] = "Фолк-барок";
            arraySpinner_podjanr[3] = "Филк";
        } else if(Janr == "Кантри"){
            arraySpinner_podjanr = new String[4];
            arraySpinner_podjanr[0] = "Блюграсс";
            arraySpinner_podjanr[1] = "Кантри-поп";
            arraySpinner_podjanr[2] = "Альт-кантри";
            arraySpinner_podjanr[3] = "Хонки-тонк";
        } else if(Janr == "Латиноамериканская музыка"){
            arraySpinner_podjanr = new String[14];
            arraySpinner_podjanr[0] = "Бачата";
            arraySpinner_podjanr[1] = "Зук";
            arraySpinner_podjanr[2] = "Кумбия";
            arraySpinner_podjanr[3] = "Ламбада";
            arraySpinner_podjanr[4] = "Мамбо";
            arraySpinner_podjanr[5] = "Меренге";
            arraySpinner_podjanr[6] = "Пачанга";
            arraySpinner_podjanr[7] = "Румба";
            arraySpinner_podjanr[8] = "Сальса";
            arraySpinner_podjanr[9] = "Самба";
            arraySpinner_podjanr[10] = "Сон";
            arraySpinner_podjanr[11] = "Танго";
            arraySpinner_podjanr[12] = "Форро";
            arraySpinner_podjanr[13] = "Ча-ча-ча";
        } else if(Janr == "Блюз"){
            arraySpinner_podjanr = new String[10];
            arraySpinner_podjanr[0] ="Сельский блюз";
            arraySpinner_podjanr[1] ="Харп-блюз";
            arraySpinner_podjanr[2] ="Техасский блюз";
            arraySpinner_podjanr[3] ="Электро-блюз";
            arraySpinner_podjanr[4] ="Вест-сайд-блюз";
            arraySpinner_podjanr[5] ="Вест-кост-блюз";
            arraySpinner_podjanr[6] ="Дельта-блюз";
            arraySpinner_podjanr[7] ="Чикагский блюз";
            arraySpinner_podjanr[8] ="Свомп-блюз";
            arraySpinner_podjanr[9] ="Зайдеко";
        } else if(Janr == "Ритм-н-блюз"){
            arraySpinner_podjanr = new String[6];
            arraySpinner_podjanr[0] ="Ду-воп";
            arraySpinner_podjanr[1] ="Соул";
            arraySpinner_podjanr[2] ="Фанк";
            arraySpinner_podjanr[3] ="Нью-джек-свинг";
            arraySpinner_podjanr[4] ="Современный ритм-н-блюз";
            arraySpinner_podjanr[5] ="Неосоул";
        } else if(Janr == "Джаз"){
            arraySpinner_podjanr = new String[34];
            arraySpinner_podjanr[0] ="Новоорлеанский или традиционный джаз";
            arraySpinner_podjanr[1] ="Хот-джаз";
            arraySpinner_podjanr[2] ="Диксиленд";
            arraySpinner_podjanr[3] ="Свинг";
            arraySpinner_podjanr[4] ="Бибоп";
            arraySpinner_podjanr[5] ="Биг-бэнд";
            arraySpinner_podjanr[6] ="Мейнстрим-джаз";
            arraySpinner_podjanr[7] ="Буги-вуги";
            arraySpinner_podjanr[8] ="Северо-Восточный джаз";
            arraySpinner_podjanr[9] ="Страйд";
            arraySpinner_podjanr[10] ="Джаз Канзас-сити";
            arraySpinner_podjanr[11] ="Джаз Западного побережья";
            arraySpinner_podjanr[12] ="Кул-джаз";
            arraySpinner_podjanr[13] ="Босса-нова";
            arraySpinner_podjanr[14] ="Ворлд-джаз";
            arraySpinner_podjanr[15] ="Прогрессив-джаз";
            arraySpinner_podjanr[16] ="Хард-боп";
            arraySpinner_podjanr[17] ="Модальный джаз";
            arraySpinner_podjanr[18] ="Соул-джаз";
            arraySpinner_podjanr[19] ="Грув-джаз";
            arraySpinner_podjanr[20] ="Фри-джаз";
            arraySpinner_podjanr[21] ="Авангардный джаз";
            arraySpinner_podjanr[22] ="Криэйтив";
            arraySpinner_podjanr[23] ="Пост-боп";
            arraySpinner_podjanr[24] ="Джаз-фьюжн";
            arraySpinner_podjanr[25] ="Джаз-фанк";
            arraySpinner_podjanr[26] ="Эйсид-джаз[8]";
            arraySpinner_podjanr[27] ="Смут-джаз";
            arraySpinner_podjanr[28] ="Ню-джаз";
            arraySpinner_podjanr[29] ="Кроссовер-джаз";
            arraySpinner_podjanr[30] ="Афро-кубинский джаз";
            arraySpinner_podjanr[31] ="Азербайджанский джаз";
            arraySpinner_podjanr[32] ="Джаз-мануш";
            arraySpinner_podjanr[33] ="Латиноамериканский джаз";
        } else if(Janr == "Электронная музыка"){
            arraySpinner_podjanr = new String[32];
            arraySpinner_podjanr[0] ="Академическая электронная музык";
            arraySpinner_podjanr[1] ="Индастриал";
            arraySpinner_podjanr[2] ="Нью-эйдж";
            arraySpinner_podjanr[3] ="Эмбиент";
            arraySpinner_podjanr[4] ="Синти-поп";
            arraySpinner_podjanr[5] ="Синт-фанк";
            arraySpinner_podjanr[6] ="Этереал";
            arraySpinner_podjanr[7] ="Электро";
            arraySpinner_podjanr[8] ="Хаус";
            arraySpinner_podjanr[9] ="Техно";
            arraySpinner_podjanr[10] ="Техно-транс";
            arraySpinner_podjanr[11] ="Spacesynth";
            arraySpinner_podjanr[12] ="Чиптюн";
            arraySpinner_podjanr[13] ="Дарквэйв";
            arraySpinner_podjanr[14] ="Вэйпорвэйв";
            arraySpinner_podjanr[15] ="New Beat";
            arraySpinner_podjanr[16] ="Брейкбит";
            arraySpinner_podjanr[17] ="Драм-н-бейс";
            arraySpinner_podjanr[18] ="Даунтемпо";
            arraySpinner_podjanr[19] ="Инди-электроника";
            arraySpinner_podjanr[20] ="IDM";
            arraySpinner_podjanr[21] ="Хардкор";
            arraySpinner_podjanr[22] ="Транс";
            arraySpinner_podjanr[23] ="Евродэнс";
            arraySpinner_podjanr[24] ="Гэридж";
            arraySpinner_podjanr[25] ="Глитч";
            arraySpinner_podjanr[26] ="Lento Violento";
            arraySpinner_podjanr[27] ="Фриформ";
            arraySpinner_podjanr[28] ="Lowercase";
            arraySpinner_podjanr[29] ="Этно-электроника";
            arraySpinner_podjanr[30] ="Чиллвейв";
            arraySpinner_podjanr[31] ="Альтернативная танцевальная музыка";
        } else if(Janr == "Рок"){
            arraySpinner_podjanr = new String[60];
            arraySpinner_podjanr[0] ="Альтернативный метал";
            arraySpinner_podjanr[1] ="Альтернативный рок";
            arraySpinner_podjanr[2] ="Анатолийский рок";
            arraySpinner_podjanr[3] ="Арт-рок";
            arraySpinner_podjanr[4] ="Бард-рок";
            arraySpinner_podjanr[5] ="Барокко-поп";
            arraySpinner_podjanr[6] ="Бит";
            arraySpinner_podjanr[7] ="Блюз-рок";
            arraySpinner_podjanr[8] ="Брит-поп";
            arraySpinner_podjanr[9] ="Виолончельный метал";
            arraySpinner_podjanr[10] ="Гаражный рок";
            arraySpinner_podjanr[11] ="Глэм-рок";
            arraySpinner_podjanr[12] ="Гранж";
            arraySpinner_podjanr[13] ="Дезерт-рок";
            arraySpinner_podjanr[14] ="Джаз-фьюжн";
            arraySpinner_podjanr[15] ="Дэнс-рок";
            arraySpinner_podjanr[16] ="Инструментальный рок";
            arraySpinner_podjanr[17] ="Индастриал-рок";
            arraySpinner_podjanr[18] ="Инди-рок";
            arraySpinner_podjanr[19] ="Камеди-рок";
            arraySpinner_podjanr[20] ="Кантри-рок";
            arraySpinner_podjanr[21] ="Келтик-рок";
            arraySpinner_podjanr[22] ="Краут-рок";
            arraySpinner_podjanr[23] ="Латин-рок";
            arraySpinner_podjanr[24] ="Математический рок";
            arraySpinner_podjanr[25] ="Метал";
            arraySpinner_podjanr[26] ="Нойз-рок";
            arraySpinner_podjanr[27] ="Паб-рок";
            arraySpinner_podjanr[28] ="Пауэр-поп";
            arraySpinner_podjanr[29] ="Панк-рок";
            arraySpinner_podjanr[30] ="Поп-рок";
            arraySpinner_podjanr[31] ="Постпанк";
            arraySpinner_podjanr[32] ="Построк";
            arraySpinner_podjanr[33] ="Прогрессивный рок";
            arraySpinner_podjanr[34] ="Прото-панк";
            arraySpinner_podjanr[35] ="Психоделический рок";
            arraySpinner_podjanr[36] ="Регги-рок";
            arraySpinner_podjanr[37] ="Рокабилли";
            arraySpinner_podjanr[38] ="Рок-н-ролл";
            arraySpinner_podjanr[39] ="Рэп-рок";
            arraySpinner_podjanr[40] ="Рэпкор";
            arraySpinner_podjanr[41] ="Сатерн-рок";
            arraySpinner_podjanr[42] ="Свомп-рок";
            arraySpinner_podjanr[43] ="Сёрф-рок";
            arraySpinner_podjanr[44] ="Софт-рок";
            arraySpinner_podjanr[45] ="Спейс-рок";
            arraySpinner_podjanr[46] ="Стадионный рок";
            arraySpinner_podjanr[47] ="Стоунер-рок";
            arraySpinner_podjanr[48] ="Трип-рок";
            arraySpinner_podjanr[49] ="Фанк-рок";
            arraySpinner_podjanr[50] ="Фермер-рок";
            arraySpinner_podjanr[51] ="Фолк-рок";
            arraySpinner_podjanr[52] ="Хард-рок";
            arraySpinner_podjanr[53] ="Хартленд-рок";
            arraySpinner_podjanr[54] ="Христианский рок";
            arraySpinner_podjanr[55] ="Хеви-метал";
            arraySpinner_podjanr[56] ="Чикано-рок";
            arraySpinner_podjanr[57] ="Экспериментальный рок";
            arraySpinner_podjanr[58] ="Электроник-рок";
            arraySpinner_podjanr[59] ="Эйсид-рок";
        } else if(Janr == "Шансон"){
            arraySpinner_podjanr = new String[1];
            arraySpinner_podjanr[0] = "Русский шансон";
        } else if(Janr == "Романс"){
            arraySpinner_podjanr = new String[4];
            arraySpinner_podjanr[0] ="Цыганский романс";
            arraySpinner_podjanr[1] ="Жестокий романс";
            arraySpinner_podjanr[2] ="Русский романс";
            arraySpinner_podjanr[3] ="Городской романс";
        } else if(Janr == "Авторская песня"){
            arraySpinner_podjanr = new String[1];
            arraySpinner_podjanr[0] = "Авторская песня";
        } else if(Janr == "Поп"){
            arraySpinner_podjanr = new String[5];
            arraySpinner_podjanr[0] ="Европоп";
            arraySpinner_podjanr[1] ="Латина";
            arraySpinner_podjanr[2] ="Синтипоп";
            arraySpinner_podjanr[3] ="Диско";
            arraySpinner_podjanr[4] ="Танцевальная музыка";
        } else if(Janr == "Хип-хоп"){
            arraySpinner_podjanr = new String[9];
            arraySpinner_podjanr[0] ="Олдскул";
            arraySpinner_podjanr[1] ="Ньюскул";
            arraySpinner_podjanr[2] ="Гангста-рэп";
            arraySpinner_podjanr[3] ="Политический хип-хоп";
            arraySpinner_podjanr[4] ="Альтернативный хип-хоп";
            arraySpinner_podjanr[5] ="Джи-фанк";
            arraySpinner_podjanr[6] ="Хорроркор";
            arraySpinner_podjanr[7] ="Южный хип-хоп";
            arraySpinner_podjanr[8] ="Грайм";
        }
        return arraySpinner_podjanr;
    }
    private ArrayAdapter<String> adapter(String[] list){
        ArrayAdapter<String> _adapter = new ArrayAdapter<String>(this, R.layout.simple_spinner_item, list);
        _adapter.setDropDownViewResource(R.layout.simple_spinner_dropdown_item);
        return _adapter;
    }
    public class MusicIntentReceiver extends BroadcastReceiver {
        final String LOG_TAG = "myLogs";

        @Override
        public void onReceive(Context context, Intent intent) {
            if (intent.getAction().equals(Intent.ACTION_HEADSET_PLUG)) {
                int state = intent.getIntExtra("state", -1);
                switch (state) {
                    case 0:
                        musicSrv.pausePlayer();
                        Log.d(LOG_TAG, "Наушники отключены");
                        break;
                    case 1:
                        musicSrv.go();
                        Log.d(LOG_TAG, "Наушники подключены");
                        break;
                    default:
                        Log.d(LOG_TAG, "Неизвестное состояние");
                }
            }
        }
    }
    private void InfoTrack(Track tab){
        try {
            redact_track = new Track(-1,".",".");
            redact_track = tab;
            LabelInfoArtist1 = (TextView) findViewById(R.id.LabelInfoArtist1);
            LabelInfoArtist2 = (TextView) findViewById(R.id.LabelInfoArtist2);
            LabelInfoArtist3 = (TextView) findViewById(R.id.LabelInfoArtist3);
            LabelInfoArtist4 = (TextView) findViewById(R.id.LabelInfoArtist4);
            LabelInfoTitle = (TextView) findViewById(R.id.LabelInfoTitle);
            LabelInfoJanr = (TextView) findViewById(R.id.LabelInfoJanr);
            LabelInfoPodJanr = (TextView) findViewById(R.id.LabelInfoPodJanr);
            LabelInfoLang = (TextView) findViewById(R.id.LabelInfoLang);
            LabelInfoNastr = (TextView) findViewById(R.id.LabelInfoNastr);
            LabelInfoDate = (TextView) findViewById(R.id.LabelInfoDate);
            if (tab.getArtist() != "") {
                LabelInfoArtist1.setText(tab.getArtist());
            } else {
                LabelInfoArtist1.setText("нет данных");
            }
            if (tab.getArtist2() != "") {
                LabelInfoArtist2.setText(tab.getArtist2());
            } else {
                LabelInfoArtist2.setText("нет данных");
            }
            if (tab.getArtist3() != "") {
                LabelInfoArtist3.setText(tab.getArtist3());
            } else {
                LabelInfoArtist3.setText("нет данных");
            }
            if (tab.getArtist4() != "") {
                LabelInfoArtist4.setText(tab.getArtist4());
            } else {
                LabelInfoArtist4.setText("нет данных");
            }
            if (tab.getTitle() != "") {
                LabelInfoTitle.setText(tab.getTitle());
            } else {
                LabelInfoTitle.setText("нет данных");
            }
            if (tab.getJanr() != "") {
                LabelInfoJanr.setText(tab.getJanr());
            } else {
                LabelInfoJanr.setText("нет данных");
            }
            if (tab.getPodJanr() != "") {
                LabelInfoPodJanr.setText(tab.getPodJanr());
            } else {
                LabelInfoPodJanr.setText("нет данных");
            }
            if (tab.getMetka1() != "") {
                LabelInfoLang.setText(tab.getMetka1());
            } else {
                LabelInfoLang.setText("нет данных");
            }
            if (tab.getMetka2() != "") {
                LabelInfoNastr.setText(tab.getMetka2());
            } else {
                LabelInfoNastr.setText("нет данных");
            }
            if (tab.getDate().toString() != "") {
                LabelInfoDate.setText(tab.getDate().toString());
            } else {
                LabelInfoDate.setText("нет данных");
            }
        }catch (Exception e){
            Log.d(LOG_TAG,"Ошибка в информации о треке: "+e.getMessage());
        }
    } //Информация о песне

    //Работа с базами данных
    public long newBaseTrack(Track tr, String table){
        Log.d(LOG_TAG, ""+ tr.getJanr()+ ", "+ tr.getMetka1()+ ", "+ tr.getPodJanr() + ", "+ tr.getMetka2());
        ContentValues cv = new ContentValues();
        SQLiteDatabase db = dbHelper.getWritableDatabase();
        try {
            cv.put("artist", tr.getArtist());
            cv.put("artist2", tr.getArtist2());
            cv.put("artist3", tr.getArtist3());
            cv.put("artist4", tr.getArtist4());
            cv.put("artist5", tr.getArtist5());
            cv.put("artist6", tr.getArtist6());
            cv.put("janr", tr.getJanr());
            cv.put("metka1", tr.getMetka1());
            cv.put("metka2", tr.getMetka2());
            cv.put("metka3", tr.getMetka3());
            cv.put("metka4", tr.getMetka4());
            cv.put("metka5", tr.getMetka5());
            cv.put("metka6", tr.getMetka6());
            cv.put("metka7", tr.getMetka7());
            cv.put("metka8", tr.getMetka8());
            cv.put("podjanr", tr.getPodJanr());
            cv.put("title", tr.getTitle());
            cv.put("date", new Date(tr.getDate().toString()).getTime());
            Log.d(LOG_TAG, "Data при записи = " + new Date(tr.getDate().toString()));
            cv.put("url", tr.getURL());
        }
        catch (Exception e){
            SimpleDateFormat sdf = new SimpleDateFormat("dd/MM/yyyy");
            String string_date = sdf.format(Calendar.getInstance().getTime());
            Date date = null;
            try {
                date = sdf.parse(string_date);
            } catch (ParseException ee) {
                ee.printStackTrace();
            }
            cv.put("date",date.getTime());
            cv.put("url", tr.getURL());
        }
        // вставляем запись и получаем ее ID
        long rowID = db.insert(table, null, cv);
        Log.d(LOG_TAG, "row inserted, ID = " + rowID);
        dbHelper.close();
        return rowID;
    }
    public ArrayList<Track> read(String table){
        ArrayList<Track> Tmp = new ArrayList<Track>();
        Track tr = new Track(-1,"","");
        try {
            Log.d(LOG_TAG, "Начало считывания: ");
            SimpleDateFormat sdf = new SimpleDateFormat("dd/MM/yyyy");
            //ContentValues cv = new ContentValues();
            SQLiteDatabase db = dbHelper.getWritableDatabase();
            Cursor c = db.query(table, null, null, null, null, null, null);
            if (c.moveToFirst()) {

                // определяем номера столбцов по имени в выборке
                int idColIndex = c.getColumnIndex("id");
                int artistColIndex = c.getColumnIndex("artist");
                int artist2ColIndex = c.getColumnIndex("artist2");
                int artist3ColIndex = c.getColumnIndex("artist3");
                int artist4ColIndex = c.getColumnIndex("artist4");
                int artist5ColIndex = c.getColumnIndex("artist5");
                int artist6ColIndex = c.getColumnIndex("artist6");
                int janrColIndex = c.getColumnIndex("janr");
                int metka1ColIndex = c.getColumnIndex("metka1");
                int metka2ColIndex = c.getColumnIndex("metka2");
                int metka3ColIndex = c.getColumnIndex("metka3");
                int metka4ColIndex = c.getColumnIndex("metka4");
                int metka5ColIndex = c.getColumnIndex("metka5");
                int metka6ColIndex = c.getColumnIndex("metka6");
                int metka7ColIndex = c.getColumnIndex("metka7");
                int metka8ColIndex = c.getColumnIndex("metka8");
                int podjanrColIndex = c.getColumnIndex("podjanr");
                int titleColIndex = c.getColumnIndex("title");
                int dateColIndex = c.getColumnIndex("date");
                int urlColIndex = c.getColumnIndex("url");
                do {
                    tr = new Track(0,  c.getString(titleColIndex) , c.getString(artistColIndex));
                    tr.setCounter(c.getLong(idColIndex));
                    tr.setArtist2(c.getString(artist2ColIndex));
                    tr.setArtist3(c.getString(artist3ColIndex));
                    tr.setArtist4(c.getString(artist4ColIndex));
                    tr.setArtist5(c.getString(artist5ColIndex));
                    tr.setArtist6(c.getString(artist6ColIndex));
                    tr.setJanr(c.getString(janrColIndex));
                    tr.setMetka1(c.getString(metka1ColIndex));
                    tr.setMetka2(c.getString(metka2ColIndex));
                    tr.setMetka3(c.getString(metka3ColIndex));
                    tr.setMetka4(c.getString(metka4ColIndex));
                    tr.setMetka5(c.getString(metka5ColIndex));
                    tr.setMetka6(c.getString(metka6ColIndex));
                    tr.setMetka7(c.getString(metka7ColIndex));
                    tr.setMetka8(c.getString(metka8ColIndex));
                    tr.setTitle(c.getString(titleColIndex));
                    tr.setPodJanr(c.getString(podjanrColIndex));
                    try {
                        java.sql.Date dt = new java.sql.Date(c.getLong(dateColIndex));
                        tr.setDate(dt);
                        //tr.setDate(sdf.parse(c.getString(dateColIndex)));
                        Log.d(LOG_TAG, ""+tr.getJanr() + tr.getPodJanr());
                        tr.setURL(c.getString(urlColIndex));
                    } catch (Exception e) {
                        Log.d(LOG_TAG, "Ошибка при чтении даты");
                    }
                    //Toast.makeText(getApplicationContext(), tr.getMetka7()+" "+tr.getMetka8(), Toast.LENGTH_SHORT).show();
                    //Toast.makeText(getApplicationContext(), tr.getArtist()+" "+ tr.getTitle() + " " + tr.getURL(), Toast.LENGTH_SHORT).show();
                    // получаем значения по номерам столбцов и пишем все в лог
                    // переход на следующую строку
                    // а если следующей нет (текущая - последняя), то false - выходим из цикла
                    //Вот тут надо добавить строку с добавлением песни в коллекцию!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                    if(tr.getID() != -1)
                        Tmp.add(tr);

                } while (c.moveToNext());
            } else
                Log.d(LOG_TAG, "0 rows");
            dbHelper.close();

        }catch (Exception e){
            Log.d(LOG_TAG, "Ошибка при чтении базы "+table+ " :"+ e.getMessage());
        }
        return Tmp;
    }
    public void delTrackBase(Track tr, String table){
        if (tr.getCounter()==0) {
            return;
        }
        ContentValues cv = new ContentValues();
        // подключаемся к БД
        SQLiteDatabase db = dbHelper.getWritableDatabase();
        int delCount = db.delete(table, "id = " + String.valueOf(tr.getCounter()), null);
    }
    public void updBase(Track tr, String table){
        if (tr.getCounter()==0) {
            return;
        }
        ContentValues cv = new ContentValues();
        // подключаемся к БД
        SQLiteDatabase db = dbHelper.getWritableDatabase();
        // подготовим значения для обновления
        cv.put("artist", tr.getArtist());
        cv.put("artist2", tr.getArtist2());
        cv.put("artist3", tr.getArtist3());
        cv.put("artist4", tr.getArtist4());
        cv.put("artist5", tr.getArtist5());
        cv.put("artist6", tr.getArtist6());
        cv.put("janr", tr.getJanr());
        cv.put("metka1", tr.getMetka1());
        cv.put("metka2", tr.getMetka2());
        cv.put("metka3", tr.getMetka3());
        cv.put("metka4", tr.getMetka4());
        cv.put("metka5", tr.getMetka5());
        cv.put("metka6", tr.getMetka6());
        cv.put("metka7", tr.getMetka7());
        cv.put("metka8", tr.getMetka8());
        cv.put("podjanr", tr.getPodJanr());
        cv.put("title", tr.getTitle());
        cv.put("date", tr.getDate().toString());
        cv.put("url", tr.getURL());
        // обновляем по id
        int updCount = db.update(table, cv, "id = ?",
                new String[] { String.valueOf(tr.getCounter()) });
    }
}

