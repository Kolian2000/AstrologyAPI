--
-- PostgreSQL database dump
--

-- Dumped from database version 16.2 (Postgres.app)
-- Dumped by pg_dump version 16.2 (Postgres.app)

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

--
-- Name: User; Type: SCHEMA; Schema: -; Owner: postgres
--

CREATE SCHEMA "User";


ALTER SCHEMA "User" OWNER TO postgres;

SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- Name: _desc; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public._desc (
    id integer NOT NULL,
    desc_name character varying(255) NOT NULL,
    desc_kind character varying(255) NOT NULL
);


ALTER TABLE public._desc OWNER TO postgres;

--
-- Name: _desc_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public._desc_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public._desc_id_seq OWNER TO postgres;

--
-- Name: _desc_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public._desc_id_seq OWNED BY public._desc.id;


--
-- Name: card; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.card (
    card_id integer NOT NULL,
    card_name character varying(255) NOT NULL,
    _number integer NOT NULL,
    suit character varying(255) NOT NULL,
    is_arcane boolean NOT NULL,
    picturepath character varying(255) NOT NULL,
    fk_desc_name character varying(255) NOT NULL
);


ALTER TABLE public.card OWNER TO postgres;

--
-- Name: card_card_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.card_card_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.card_card_id_seq OWNER TO postgres;

--
-- Name: card_card_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.card_card_id_seq OWNED BY public.card.card_id;


--
-- Name: horoscopes; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.horoscopes (
    horoscope_id integer NOT NULL,
    can_get_horoscope boolean,
    horoscope_time timestamp without time zone,
    username character varying(50),
    zodiac_sign character varying(20),
    time_zone character varying(30)
);


ALTER TABLE public.horoscopes OWNER TO postgres;

--
-- Name: horoscopes_horoscope_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.horoscopes_horoscope_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.horoscopes_horoscope_id_seq OWNER TO postgres;

--
-- Name: horoscopes_horoscope_id_seq1; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.horoscopes_horoscope_id_seq1
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.horoscopes_horoscope_id_seq1 OWNER TO postgres;

--
-- Name: horoscopes_horoscope_id_seq1; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.horoscopes_horoscope_id_seq1 OWNED BY public.horoscopes.horoscope_id;


--
-- Name: users; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.users (
    user_id integer NOT NULL,
    username character varying(255) NOT NULL,
    email character varying(255),
    password_hash character varying(255) NOT NULL,
    response_count integer DEFAULT 3,
    created_at timestamp without time zone DEFAULT CURRENT_TIMESTAMP,
    can_get_horoscope boolean
);


ALTER TABLE public.users OWNER TO postgres;

--
-- Name: users_user_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.users_user_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.users_user_id_seq OWNER TO postgres;

--
-- Name: users_user_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.users_user_id_seq OWNED BY public.users.user_id;


--
-- Name: usersы; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."usersы" (
    user_id integer NOT NULL,
    username character varying(255) NOT NULL,
    email character varying(255),
    password_hash character varying(255) NOT NULL,
    created_at timestamp without time zone DEFAULT CURRENT_TIMESTAMP
);


ALTER TABLE public."usersы" OWNER TO postgres;

--
-- Name: usersы_user_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public."usersы_user_id_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public."usersы_user_id_seq" OWNER TO postgres;

--
-- Name: usersы_user_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public."usersы_user_id_seq" OWNED BY public."usersы".user_id;


--
-- Name: _desc id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public._desc ALTER COLUMN id SET DEFAULT nextval('public._desc_id_seq'::regclass);


--
-- Name: card card_id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.card ALTER COLUMN card_id SET DEFAULT nextval('public.card_card_id_seq'::regclass);


--
-- Name: horoscopes horoscope_id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.horoscopes ALTER COLUMN horoscope_id SET DEFAULT nextval('public.horoscopes_horoscope_id_seq1'::regclass);


--
-- Name: users user_id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.users ALTER COLUMN user_id SET DEFAULT nextval('public.users_user_id_seq'::regclass);


--
-- Name: usersы user_id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."usersы" ALTER COLUMN user_id SET DEFAULT nextval('public."usersы_user_id_seq"'::regclass);


--
-- Data for Name: _desc; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public._desc (id, desc_name, desc_kind) FROM stdin;
1	GothicDesc	Classic
3	FirstDesc	Classic
\.


--
-- Data for Name: card; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.card (card_id, card_name, _number, suit, is_arcane, picturepath, fk_desc_name) FROM stdin;
79	Дурак	0	Major	t	Major00.jpg	GothicDesc
80	Маг	1	Major	t	Major01.jpg	GothicDesc
81	Верховная Жрица	2	Major	t	Major02.jpg	GothicDesc
82	Императрица	3	Major	t	Major03.jpg	GothicDesc
83	Император	4	Major	t	Major04.jpg	GothicDesc
84	Жрица	5	Major	t	Major05.jpg	GothicDesc
85	Влюблённые	6	Major	t	Major06.jpg	GothicDesc
86	Колесница	7	Major	t	Major07.jpg	GothicDesc
87	Сила	8	Major	t	Major08.jpg	GothicDesc
88	Отшельник	9	Major	t	Major09.jpg	GothicDesc
89	Колесо Фортуны	10	Major	t	Major10.jpg	GothicDesc
90	Справедливость	11	Major	t	Major11.jpg	GothicDesc
91	Повешенный	12	Major	t	Major12.jpg	GothicDesc
92	Смерть	13	Major	t	Major13.jpg	GothicDesc
93	Умеренность	14	Major	t	Major14.jpg	GothicDesc
94	Дьявол	15	Major	t	Major15.jpg	GothicDesc
95	Башня	16	Major	t	Major16.jpg	GothicDesc
96	Звезда	17	Major	t	Major17.jpg	GothicDesc
97	Луна	18	Major	t	Major18.jpg	GothicDesc
98	Солнце	19	Major	t	Major19.jpg	GothicDesc
99	Суд	20	Major	t	Major20.jpg	GothicDesc
100	Мир	21	Major	t	Major21.jpg	GothicDesc
101	Туз Пентаклей	1	Coins	f	Coins01.jpg	GothicDesc
102	2 Пентаклей	2	Coins	f	Coins02.jpg	GothicDesc
103	3 Пентаклей	3	Coins	f	Coins03.jpg	GothicDesc
104	4 Пентаклей	4	Coins	f	Coins04.jpg	GothicDesc
105	5 Пентаклей	5	Coins	f	Coins05.jpg	GothicDesc
106	6 Пентаклей	6	Coins	f	Coins06.jpg	GothicDesc
107	7 Пентаклей	7	Coins	f	Coins07.jpg	GothicDesc
108	8 Пентаклей	8	Coins	f	Coins08.jpg	GothicDesc
109	9 Пентаклей	9	Coins	f	Coins09.jpg	GothicDesc
110	10 Пентаклей	10	Coins	f	Coins10.jpg	GothicDesc
111	Паж Пентаклей	11	Coins	f	CoinsPage.jpg	GothicDesc
112	Рыцарь Пентаклей	12	Coins	f	CoinsKnight.jpg	GothicDesc
113	Королева Пентаклей	13	Coins	f	CoinsQueen.jpg	GothicDesc
114	Король Пентаклей	14	Coins	f	CoinsKing.jpg	GothicDesc
115	Туз Кубков	1	Cups	f	Cups01.jpg	GothicDesc
116	2 Кубков	2	Cups	f	Cups02.jpg	GothicDesc
117	3 Кубков	3	Cups	f	Cups03.jpg	GothicDesc
118	4 Кубков	4	Cups	f	Cups04.jpg	GothicDesc
119	5 Кубков	5	Cups	f	Cups05.jpg	GothicDesc
120	6 Кубков	6	Cups	f	Cups06.jpg	GothicDesc
121	7 Кубков	7	Cups	f	Cups07.jpg	GothicDesc
122	8 Кубков	8	Cups	f	Cups08.jpg	GothicDesc
123	9 Кубков	9	Cups	f	Cups09.jpg	GothicDesc
124	10 Кубков	10	Cups	f	Cups10.jpg	GothicDesc
125	Паж Кубков	11	Cups	f	CupsPage.jpg	GothicDesc
126	Рыцарь Кубков	12	Cups	f	CupsKnight.jpg	GothicDesc
127	Королева Кубков	13	Cups	f	CupsQueen.jpg	GothicDesc
128	Король Кубков	14	Cups	f	CupsKing.jpg	GothicDesc
129	Туз Мечей	1	Swords	f	Swords01.jpg	GothicDesc
130	2 Мечей	2	Swords	f	Swords02.jpg	GothicDesc
131	3 Мечей	3	Swords	f	Swords03.jpg	GothicDesc
132	4 Мечей	4	Swords	f	Swords04.jpg	GothicDesc
133	5 Мечей	5	Swords	f	Swords05.jpg	GothicDesc
134	6 Мечей	6	Swords	f	Swords06.jpg	GothicDesc
135	7 Мечей	7	Swords	f	Swords07.jpg	GothicDesc
136	8 Мечей	8	Swords	f	Swords08.jpg	GothicDesc
137	9 Мечей	9	Swords	f	Swords09.jpg	GothicDesc
138	10 Мечей	10	Swords	f	Swords10.jpg	GothicDesc
139	Паж Мечей	11	Swords	f	SwordsPage.jpg	GothicDesc
140	Рыцарь Мечей	12	Swords	f	SwordsKnight.jpg	GothicDesc
141	Королева Мечей	13	Swords	f	SwordsQueen.jpg	GothicDesc
142	Король Мечей	14	Swords	f	SwordsKing.jpg	GothicDesc
143	Туз Жезлов	1	Wands	f	Wands01.jpg	GothicDesc
144	2 Жезлов	2	Wands	f	Wands02.jpg	GothicDesc
145	3 Жезлов	3	Wands	f	Wands03.jpg	GothicDesc
146	4 Жезлов	4	Wands	f	Wands04.jpg	GothicDesc
147	5 Жезлов	5	Wands	f	Wands05.jpg	GothicDesc
148	6 Жезлов	6	Wands	f	Wands06.jpg	GothicDesc
149	7 Жезлов	7	Wands	f	Wands07.jpg	GothicDesc
150	8 Жезлов	8	Wands	f	Wands08.jpg	GothicDesc
151	9 Жезлов	9	Wands	f	Wands09.jpg	GothicDesc
152	10 Жезлов	10	Wands	f	Wands10.jpg	GothicDesc
153	Паж Жезлов	11	Wands	f	WandsPage.jpg	GothicDesc
154	Рыцарь Жезлов	12	Wands	f	WandsKnight.jpg	GothicDesc
155	Королева Жезлов	13	Wands	f	WandsQueen.jpg	GothicDesc
156	Король Жезлов	14	Wands	f	WandsKing.jpg	GothicDesc
469	Дурак	0	Major	t	Major00.jpg	FirstDesc
470	Маг	1	Major	t	Major01.jpg	FirstDesc
471	Верховная Жрица	2	Major	t	Major02.jpg	FirstDesc
472	Императрица	3	Major	t	Major03.jpg	FirstDesc
473	Император	4	Major	t	Major04.jpg	FirstDesc
474	Жрица	5	Major	t	Major05.jpg	FirstDesc
475	Влюблённые	6	Major	t	Major06.jpg	FirstDesc
476	Колесница	7	Major	t	Major07.jpg	FirstDesc
477	Сила	8	Major	t	Major08.jpg	FirstDesc
478	Отшельник	9	Major	t	Major09.jpg	FirstDesc
479	Колесо Фортуны	10	Major	t	Major10.jpg	FirstDesc
480	Справедливость	11	Major	t	Major11.jpg	FirstDesc
481	Повешенный	12	Major	t	Major12.jpg	FirstDesc
482	Смерть	13	Major	t	Major13.jpg	FirstDesc
483	Умеренность	14	Major	t	Major14.jpg	FirstDesc
484	Дьявол	15	Major	t	Major15.jpg	FirstDesc
485	Башня	16	Major	t	Major16.jpg	FirstDesc
486	Звезда	17	Major	t	Major17.jpg	FirstDesc
487	Луна	18	Major	t	Major18.jpg	FirstDesc
488	Солнце	19	Major	t	Major19.jpg	FirstDesc
489	Суд	20	Major	t	Major20.jpg	FirstDesc
490	Мир	21	Major	t	Major21.jpg	FirstDesc
491	Туз Пентаклей	1	Coins	f	Coins01.jpg	FirstDesc
492	2 Пентаклей	2	Coins	f	Coins02.jpg	FirstDesc
493	3 Пентаклей	3	Coins	f	Coins03.jpg	FirstDesc
494	4 Пентаклей	4	Coins	f	Coins04.jpg	FirstDesc
495	5 Пентаклей	5	Coins	f	Coins05.jpg	FirstDesc
496	6 Пентаклей	6	Coins	f	Coins06.jpg	FirstDesc
497	7 Пентаклей	7	Coins	f	Coins07.jpg	FirstDesc
498	8 Пентаклей	8	Coins	f	Coins08.jpg	FirstDesc
499	9 Пентаклей	9	Coins	f	Coins09.jpg	FirstDesc
500	10 Пентаклей	10	Coins	f	Coins10.jpg	FirstDesc
501	Паж Пентаклей	11	Coins	f	CoinsPage.jpg	FirstDesc
502	Рыцарь Пентаклей	12	Coins	f	CoinsKnight.jpg	FirstDesc
503	Королева Пентаклей	13	Coins	f	CoinsQueen.jpg	FirstDesc
504	Король Пентаклей	14	Coins	f	CoinsKing.jpg	FirstDesc
505	Туз Кубков	1	Cups	f	Cups01.jpg	FirstDesc
506	2 Кубков	2	Cups	f	Cups02.jpg	FirstDesc
507	3 Кубков	3	Cups	f	Cups03.jpg	FirstDesc
508	4 Кубков	4	Cups	f	Cups04.jpg	FirstDesc
509	5 Кубков	5	Cups	f	Cups05.jpg	FirstDesc
510	6 Кубков	6	Cups	f	Cups06.jpg	FirstDesc
511	7 Кубков	7	Cups	f	Cups07.jpg	FirstDesc
512	8 Кубков	8	Cups	f	Cups08.jpg	FirstDesc
513	9 Кубков	9	Cups	f	Cups09.jpg	FirstDesc
514	10 Кубков	10	Cups	f	Cups10.jpg	FirstDesc
515	Паж Кубков	11	Cups	f	CupsPage.jpg	FirstDesc
516	Рыцарь Кубков	12	Cups	f	CupsKnight.jpg	FirstDesc
517	Королева Кубков	13	Cups	f	CupsQueen.jpg	FirstDesc
518	Король Кубков	14	Cups	f	CupsKing.jpg	FirstDesc
519	Туз Мечей	1	Swords	f	Swords01.jpg	FirstDesc
520	2 Мечей	2	Swords	f	Swords02.jpg	FirstDesc
521	3 Мечей	3	Swords	f	Swords03.jpg	FirstDesc
522	4 Мечей	4	Swords	f	Swords04.jpg	FirstDesc
523	5 Мечей	5	Swords	f	Swords05.jpg	FirstDesc
524	6 Мечей	6	Swords	f	Swords06.jpg	FirstDesc
525	7 Мечей	7	Swords	f	Swords07.jpg	FirstDesc
526	8 Мечей	8	Swords	f	Swords08.jpg	FirstDesc
527	9 Мечей	9	Swords	f	Swords09.jpg	FirstDesc
528	10 Мечей	10	Swords	f	Swords10.jpg	FirstDesc
529	Паж Мечей	11	Swords	f	SwordsPage.jpg	FirstDesc
530	Рыцарь Мечей	12	Swords	f	SwordsKnight.jpg	FirstDesc
531	Королева Мечей	13	Swords	f	SwordsQueen.jpg	FirstDesc
532	Король Мечей	14	Swords	f	SwordsKing.jpg	FirstDesc
533	Туз Жезлов	1	Wands	f	Wands01.jpg	FirstDesc
534	2 Жезлов	2	Wands	f	Wands02.jpg	FirstDesc
535	3 Жезлов	3	Wands	f	Wands03.jpg	FirstDesc
536	4 Жезлов	4	Wands	f	Wands04.jpg	FirstDesc
537	5 Жезлов	5	Wands	f	Wands05.jpg	FirstDesc
538	6 Жезлов	6	Wands	f	Wands06.jpg	FirstDesc
539	7 Жезлов	7	Wands	f	Wands07.jpg	FirstDesc
540	8 Жезлов	8	Wands	f	Wands08.jpg	FirstDesc
541	9 Жезлов	9	Wands	f	Wands09.jpg	FirstDesc
542	10 Жезлов	10	Wands	f	Wands10.jpg	FirstDesc
543	Паж Жезлов	11	Wands	f	WandsPage.jpg	FirstDesc
544	Рыцарь Жезлов	12	Wands	f	WandsKnight.jpg	FirstDesc
545	Королева Жезлов	13	Wands	f	WandsQueen.jpg	FirstDesc
546	Король Жезлов	14	Wands	f	WandsKing.jpg	FirstDesc
\.


--
-- Data for Name: horoscopes; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.horoscopes (horoscope_id, can_get_horoscope, horoscope_time, username, zodiac_sign, time_zone) FROM stdin;
9	t	2024-03-05 13:00:00	176870702	Дева	Europe/Podgorica
\.


--
-- Data for Name: users; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.users (user_id, username, email, password_hash, response_count, created_at, can_get_horoscope) FROM stdin;
2	0	\N	$2a$11$qyn2hOG4nULZgEKQn0H1E.hxq9dfZTBuUSGb3zc8oE.0a4Y3ymB6a	3	2024-02-05 15:58:07.216897	\N
4	stream	\N	123	3	2024-02-05 19:04:05.821631	\N
8	595932270	\N	$2a$11$w2//gmQAoHaQ.zZEn44g5ubx5XvAk6ouDbfE9Qnwj.hhGamnO8yCW	3	2024-02-27 14:57:58.150471	t
9	176870702	\N	$2a$11$KHXk0qBM46qSnnZuhr7nsejuVmYMCRpNg/d5NO6DHgbjPZfzLiqsm	3	2024-02-29 17:13:01.492058	t
\.


--
-- Data for Name: usersы; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."usersы" (user_id, username, email, password_hash, created_at) FROM stdin;
2	0	\N	$2a$11$sDXwza4NWPM8iruAD2O0HuUXy737I0qlMlqkWDuIGS1syLdSOjqOq	2024-01-31 15:54:27.174884
\.


--
-- Name: _desc_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public._desc_id_seq', 3, true);


--
-- Name: card_card_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.card_card_id_seq', 546, true);


--
-- Name: horoscopes_horoscope_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.horoscopes_horoscope_id_seq', 1, false);


--
-- Name: horoscopes_horoscope_id_seq1; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.horoscopes_horoscope_id_seq1', 9, true);


--
-- Name: users_user_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.users_user_id_seq', 9, true);


--
-- Name: usersы_user_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public."usersы_user_id_seq"', 4, true);


--
-- Name: _desc _desc_desc_name_key; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public._desc
    ADD CONSTRAINT _desc_desc_name_key UNIQUE (desc_name);


--
-- Name: _desc _desc_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public._desc
    ADD CONSTRAINT _desc_pkey PRIMARY KEY (id);


--
-- Name: card card_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.card
    ADD CONSTRAINT card_pkey PRIMARY KEY (card_id);


--
-- Name: horoscopes horoscopes_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.horoscopes
    ADD CONSTRAINT horoscopes_pkey PRIMARY KEY (horoscope_id);


--
-- Name: users unique_username; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.users
    ADD CONSTRAINT unique_username UNIQUE (username);


--
-- Name: users users_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.users
    ADD CONSTRAINT users_pkey PRIMARY KEY (user_id);


--
-- Name: users users_username_key; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.users
    ADD CONSTRAINT users_username_key UNIQUE (username);


--
-- Name: usersы usersы_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."usersы"
    ADD CONSTRAINT "usersы_pkey" PRIMARY KEY (user_id);


--
-- Name: usersы usersы_username_key; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."usersы"
    ADD CONSTRAINT "usersы_username_key" UNIQUE (username);


--
-- Name: card card_fk_desc_name_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.card
    ADD CONSTRAINT card_fk_desc_name_fkey FOREIGN KEY (fk_desc_name) REFERENCES public._desc(desc_name);


--
-- Name: horoscopes horoscopes_username_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.horoscopes
    ADD CONSTRAINT horoscopes_username_fkey FOREIGN KEY (username) REFERENCES public.users(username);


--
-- PostgreSQL database dump complete
--

