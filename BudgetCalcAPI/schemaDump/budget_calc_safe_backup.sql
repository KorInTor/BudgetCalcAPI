--
-- PostgreSQL database dump
--

-- Dumped from database version 16.2
-- Dumped by pg_dump version 16.2

-- Started on 2025-01-22 01:38:33

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
-- TOC entry 5 (class 2615 OID 92918)
-- Name: public; Type: SCHEMA; Schema: -; Owner: -
--

CREATE SCHEMA public;


--
-- TOC entry 4808 (class 0 OID 0)
-- Dependencies: 5
-- Name: SCHEMA public; Type: COMMENT; Schema: -; Owner: -
--

COMMENT ON SCHEMA public IS 'standard public schema';


--
-- TOC entry 225 (class 1255 OID 93018)
-- Name: create_family_group(); Type: FUNCTION; Schema: public; Owner: -
--

CREATE FUNCTION public.create_family_group() RETURNS trigger
    LANGUAGE plpgsql
    AS $$
BEGIN
    -- Вставка новой записи в таблицу family_group
    INSERT INTO public.family_group DEFAULT VALUES;
    
    -- Присваивание нового family_group_id новому пользователю
    NEW.family_group_id := currval('public.family_group_id_seq');
    
    RETURN NEW;
END;
$$;


SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- TOC entry 223 (class 1259 OID 92978)
-- Name: family_group; Type: TABLE; Schema: public; Owner: -
--

CREATE TABLE public.family_group (
    id integer NOT NULL
);


--
-- TOC entry 224 (class 1259 OID 92981)
-- Name: family_group_id_seq; Type: SEQUENCE; Schema: public; Owner: -
--

CREATE SEQUENCE public.family_group_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


--
-- TOC entry 4809 (class 0 OID 0)
-- Dependencies: 224
-- Name: family_group_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: -
--

ALTER SEQUENCE public.family_group_id_seq OWNED BY public.family_group.id;


--
-- TOC entry 215 (class 1259 OID 92919)
-- Name: transaction; Type: TABLE; Schema: public; Owner: -
--

CREATE TABLE public.transaction (
    id bigint NOT NULL,
    category_id integer,
    user_id integer NOT NULL,
    created_at timestamp with time zone DEFAULT now() NOT NULL,
    amount money NOT NULL
);


--
-- TOC entry 217 (class 1259 OID 92924)
-- Name: transaction_category; Type: TABLE; Schema: public; Owner: -
--

CREATE TABLE public.transaction_category (
    id integer NOT NULL,
    name character varying NOT NULL,
    type_id integer
);


--
-- TOC entry 218 (class 1259 OID 92929)
-- Name: transaction_category_id_seq; Type: SEQUENCE; Schema: public; Owner: -
--

CREATE SEQUENCE public.transaction_category_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


--
-- TOC entry 4810 (class 0 OID 0)
-- Dependencies: 218
-- Name: transaction_category_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: -
--

ALTER SEQUENCE public.transaction_category_id_seq OWNED BY public.transaction_category.id;


--
-- TOC entry 216 (class 1259 OID 92923)
-- Name: transaction_id_seq; Type: SEQUENCE; Schema: public; Owner: -
--

CREATE SEQUENCE public.transaction_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


--
-- TOC entry 4811 (class 0 OID 0)
-- Dependencies: 216
-- Name: transaction_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: -
--

ALTER SEQUENCE public.transaction_id_seq OWNED BY public.transaction.id;


--
-- TOC entry 219 (class 1259 OID 92930)
-- Name: transaction_type; Type: TABLE; Schema: public; Owner: -
--

CREATE TABLE public.transaction_type (
    id integer NOT NULL,
    name character varying NOT NULL,
    familygroup_owner_id integer NOT NULL
);


--
-- TOC entry 220 (class 1259 OID 92935)
-- Name: transaction_type_id_seq; Type: SEQUENCE; Schema: public; Owner: -
--

CREATE SEQUENCE public.transaction_type_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


--
-- TOC entry 4812 (class 0 OID 0)
-- Dependencies: 220
-- Name: transaction_type_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: -
--

ALTER SEQUENCE public.transaction_type_id_seq OWNED BY public.transaction_type.id;


--
-- TOC entry 221 (class 1259 OID 92936)
-- Name: user; Type: TABLE; Schema: public; Owner: -
--

CREATE TABLE public."user" (
    id integer NOT NULL,
    family_group_id integer,
    fullname character varying NOT NULL,
    username character varying NOT NULL,
    password_hash character varying NOT NULL
);


--
-- TOC entry 222 (class 1259 OID 92941)
-- Name: user_id_seq; Type: SEQUENCE; Schema: public; Owner: -
--

CREATE SEQUENCE public.user_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


--
-- TOC entry 4813 (class 0 OID 0)
-- Dependencies: 222
-- Name: user_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: -
--

ALTER SEQUENCE public.user_id_seq OWNED BY public."user".id;


--
-- TOC entry 4630 (class 2604 OID 92982)
-- Name: family_group id; Type: DEFAULT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.family_group ALTER COLUMN id SET DEFAULT nextval('public.family_group_id_seq'::regclass);


--
-- TOC entry 4625 (class 2604 OID 92942)
-- Name: transaction id; Type: DEFAULT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.transaction ALTER COLUMN id SET DEFAULT nextval('public.transaction_id_seq'::regclass);


--
-- TOC entry 4627 (class 2604 OID 92943)
-- Name: transaction_category id; Type: DEFAULT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.transaction_category ALTER COLUMN id SET DEFAULT nextval('public.transaction_category_id_seq'::regclass);


--
-- TOC entry 4628 (class 2604 OID 92944)
-- Name: transaction_type id; Type: DEFAULT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.transaction_type ALTER COLUMN id SET DEFAULT nextval('public.transaction_type_id_seq'::regclass);


--
-- TOC entry 4629 (class 2604 OID 92945)
-- Name: user id; Type: DEFAULT; Schema: public; Owner: -
--

ALTER TABLE ONLY public."user" ALTER COLUMN id SET DEFAULT nextval('public.user_id_seq'::regclass);


--
-- TOC entry 4801 (class 0 OID 92978)
-- Dependencies: 223
-- Data for Name: family_group; Type: TABLE DATA; Schema: public; Owner: -
--



--
-- TOC entry 4793 (class 0 OID 92919)
-- Dependencies: 215
-- Data for Name: transaction; Type: TABLE DATA; Schema: public; Owner: -
--



--
-- TOC entry 4795 (class 0 OID 92924)
-- Dependencies: 217
-- Data for Name: transaction_category; Type: TABLE DATA; Schema: public; Owner: -
--



--
-- TOC entry 4797 (class 0 OID 92930)
-- Dependencies: 219
-- Data for Name: transaction_type; Type: TABLE DATA; Schema: public; Owner: -
--



--
-- TOC entry 4799 (class 0 OID 92936)
-- Dependencies: 221
-- Data for Name: user; Type: TABLE DATA; Schema: public; Owner: -
--



--
-- TOC entry 4814 (class 0 OID 0)
-- Dependencies: 224
-- Name: family_group_id_seq; Type: SEQUENCE SET; Schema: public; Owner: -
--

SELECT pg_catalog.setval('public.family_group_id_seq', 1, false);


--
-- TOC entry 4815 (class 0 OID 0)
-- Dependencies: 218
-- Name: transaction_category_id_seq; Type: SEQUENCE SET; Schema: public; Owner: -
--

SELECT pg_catalog.setval('public.transaction_category_id_seq', 1, false);


--
-- TOC entry 4816 (class 0 OID 0)
-- Dependencies: 216
-- Name: transaction_id_seq; Type: SEQUENCE SET; Schema: public; Owner: -
--

SELECT pg_catalog.setval('public.transaction_id_seq', 1, false);


--
-- TOC entry 4817 (class 0 OID 0)
-- Dependencies: 220
-- Name: transaction_type_id_seq; Type: SEQUENCE SET; Schema: public; Owner: -
--

SELECT pg_catalog.setval('public.transaction_type_id_seq', 1, false);


--
-- TOC entry 4818 (class 0 OID 0)
-- Dependencies: 222
-- Name: user_id_seq; Type: SEQUENCE SET; Schema: public; Owner: -
--

SELECT pg_catalog.setval('public.user_id_seq', 1, false);


--
-- TOC entry 4642 (class 2606 OID 92987)
-- Name: family_group family_group_pk; Type: CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.family_group
    ADD CONSTRAINT family_group_pk PRIMARY KEY (id);


--
-- TOC entry 4634 (class 2606 OID 92947)
-- Name: transaction_category transaction_category_pk; Type: CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.transaction_category
    ADD CONSTRAINT transaction_category_pk PRIMARY KEY (id);


--
-- TOC entry 4632 (class 2606 OID 92949)
-- Name: transaction transaction_pk; Type: CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.transaction
    ADD CONSTRAINT transaction_pk PRIMARY KEY (id);


--
-- TOC entry 4636 (class 2606 OID 92951)
-- Name: transaction_type transaction_type_pk; Type: CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.transaction_type
    ADD CONSTRAINT transaction_type_pk PRIMARY KEY (id);


--
-- TOC entry 4638 (class 2606 OID 92953)
-- Name: user user_pk; Type: CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public."user"
    ADD CONSTRAINT user_pk PRIMARY KEY (id);


--
-- TOC entry 4640 (class 2606 OID 92971)
-- Name: user user_username_unique; Type: CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public."user"
    ADD CONSTRAINT user_username_unique UNIQUE (username);


--
-- TOC entry 4649 (class 2620 OID 93019)
-- Name: user before_user_insert; Type: TRIGGER; Schema: public; Owner: -
--

CREATE TRIGGER before_user_insert BEFORE INSERT ON public."user" FOR EACH ROW EXECUTE FUNCTION public.create_family_group();


--
-- TOC entry 4645 (class 2606 OID 92954)
-- Name: transaction_category transaction_category_transaction_type_fk; Type: FK CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.transaction_category
    ADD CONSTRAINT transaction_category_transaction_type_fk FOREIGN KEY (type_id) REFERENCES public.transaction_type(id) ON UPDATE CASCADE ON DELETE SET NULL;


--
-- TOC entry 4643 (class 2606 OID 92959)
-- Name: transaction transaction_transaction_category_fk; Type: FK CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.transaction
    ADD CONSTRAINT transaction_transaction_category_fk FOREIGN KEY (category_id) REFERENCES public.transaction_category(id) ON UPDATE CASCADE ON DELETE SET NULL;


--
-- TOC entry 4646 (class 2606 OID 93003)
-- Name: transaction_type transaction_type_family_group_fk; Type: FK CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.transaction_type
    ADD CONSTRAINT transaction_type_family_group_fk FOREIGN KEY (familygroup_owner_id) REFERENCES public.family_group(id) ON UPDATE CASCADE ON DELETE CASCADE;


--
-- TOC entry 4647 (class 2606 OID 92973)
-- Name: transaction_type transaction_type_user_fk; Type: FK CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.transaction_type
    ADD CONSTRAINT transaction_type_user_fk FOREIGN KEY (familygroup_owner_id) REFERENCES public."user"(id) ON UPDATE CASCADE ON DELETE CASCADE;


--
-- TOC entry 4644 (class 2606 OID 92964)
-- Name: transaction transaction_user_fk; Type: FK CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.transaction
    ADD CONSTRAINT transaction_user_fk FOREIGN KEY (user_id) REFERENCES public."user"(id) ON UPDATE CASCADE ON DELETE CASCADE;


--
-- TOC entry 4648 (class 2606 OID 92998)
-- Name: user user_family_group_fk; Type: FK CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public."user"
    ADD CONSTRAINT user_family_group_fk FOREIGN KEY (family_group_id) REFERENCES public.family_group(id) ON UPDATE CASCADE ON DELETE SET NULL;


-- Completed on 2025-01-22 01:38:33

--
-- PostgreSQL database dump complete
--

