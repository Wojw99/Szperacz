######################TOKENY######################
from enum import Enum

class Token(Enum):
    EOS = 0
    ANY = 1
    AT_BOL = 2
    AT_EOL = 3
    CCL_END = 4
    CCL_START = 5
    CLOSE_CURLY = 6
    CLOSE_PAREN = 7
    CLOSURE = 8
    DASH = 9
    END_OF_INPUT = 10
    L = 11
    OPEN_CURLY = 12
    OPEN_PAREN = 13
    OPTIONAL = 14
    OR = 15
    PLUS_CLOSE = 16


Tokens = {
    '.': Token.ANY,
    '^': Token.AT_BOL,
    '$': Token.AT_EOL,
    ']': Token.CCL_END,
    '[': Token.CCL_START,
    '}': Token.CLOSE_CURLY,
    ')': Token.CLOSE_PAREN,
    '*': Token.CLOSURE,
    '-': Token.DASH,
    '{': Token.OPEN_CURLY,
    '(': Token.OPEN_PAREN,
    '?': Token.OPTIONAL,
    '|': Token.OR,
    '+': Token.PLUS_CLOSE,
}
######################SŁOWNIK###################
def scanner():
    i = input('Please enter a regular expression: ')
    return i

class Lexer(object):
    def __init__(self, pattern):
        self.pattern = pattern
        self.lexeme = ''
        self.pos = 0
        self.isescape = False
        self.current_token = None

    def advance(self):
        pos = self.pos
        pattern = self.pattern
        if pos > len(pattern) - 1:
            self.current_token = Token.EOS
            return Token.EOS

        text = self.lexeme = pattern[pos]
        if text == '\\':
            self.isescape = not self.isescape
            self.pos = self.pos + 1
            self.current_token = self.handle_escape()
        else:
            self.current_token = self.handle_semantic_l(text)

        return self.current_token

    def handle_escape(self):
        expr = self.pattern.lower()
        pos = self.pos
        ev = {
            '\0': '\\',
            'b': '\b',
            'f': '\f',
            'n': '\n',
            's': ' ',
            't': '\t',
            'e': '\033',
        }
        rval = ev.get(expr[pos])
        if rval is None:
            if expr[pos] == '^':
                rval = self.handle_tip()
            elif expr[pos] == 'O':
                rval = self.handle_oct()
            elif expr[pos] == 'X':
                rval = self.handle_hex()
            else:
                rval = expr[pos]
        self.pos = self.pos + 1
        self.lexeme = rval
        return Token.L

    def handle_semantic_l(self, text):
        self.pos = self.pos + 1
        return Tokens.get(text, Token.L)

    def handle_tip(self):
        self.pos = self.pos + 1
        return self.pattern[self.pos] - '@'

    def handle_oct(self):
        return 1

    def handle_hex(self):
        return 1

    def match(self, token):
        return self.current_token == token



######################NFA######################
#Used Variables
EPSILON = -1
CCL = -2
EMPTY = -3
ASCII_COUNT = 10000 #127
########################
#NFA CLASS
class Nfa(object):
    STATUS_NUM = 0.0

    def __init__(self):
        self.edge = EPSILON
        self.next_1 = None
        self.next_2 = None
        self.visited = False
        self.input_set = set()
        self.set_status_num()

    def set_status_num(self):
        self.status_num = Nfa.STATUS_NUM
        Nfa.STATUS_NUM = Nfa.STATUS_NUM + 1

    def set_input_set(self):
        self.input_set = set()
        for i in range(ASCII_COUNT):
            self.input_set.add(chr(i))
#NFA CLASS PAIR
class NfaPair(object):
    def __init__(self):
        self.start_node = None
        self.end_node = None
######################NFA CONSTRUCTOR######################
lexer = None


def _pattern(pattern_string):
    global lexer
    lexer = Lexer(pattern_string)
    lexer.advance()
    nfa_pair = NfaPair()
    group(nfa_pair)

    return nfa_pair.start_node

def term(pair_out):
    if lexer.match(Token.L):
        nfa_single_char(pair_out)
    elif lexer.match(Token.ANY):
        nfa_dot_char(pair_out)
    elif lexer.match(Token.CCL_START):
        nfa_set_nega_char(pair_out)


def nfa_single_char(pair_out):
    if not lexer.match(Token.L):
        return False

    start = pair_out.start_node = Nfa()
    pair_out.end_node = pair_out.start_node.next_1 = Nfa()
    start.edge = lexer.lexeme
    lexer.advance()
    return True


def nfa_dot_char(pair_out):
    if not lexer.match(Token.ANY):
        return False

    start = pair_out.start_node = Nfa()
    pair_out.end_node = pair_out.start_node.next_1 = Nfa()
    start.edge = CCL
    start.set_input_set()

    lexer.advance()
    return False


def nfa_set_char(pair_out):
    if not lexer.match(Token.CCL_START):
        return False

    start = pair_out.start_node = Nfa()
    pair_out.end_node = pair_out.start_node.next_1 = Nfa()
    start.edge = CCL
    start.input_set = set()
    dodash(start.input_set)

    lexer.advance()
    return True


def nfa_set_nega_char(pair_out):
    if not lexer.match(Token.CCL_START):
        return False
    
    neagtion = False
    lexer.advance()
    if lexer.match(Token.AT_BOL):
        neagtion = True
    
    start = pair_out.start_node = Nfa()
    start.next_1 = pair_out.end_node = Nfa()
    start.edge = CCL
    dodash(start.input_set)

    if neagtion:
        char_set_inversion(start.input_set)

    lexer.advance()
    return True


def char_set_inversion(input_set):
    for i in range(ASCII_COUNT):
        c = chr(i)
        if c not in input_set:
            input_set.add(c)


def dodash(input_set):
    first = ''
    while not lexer.match(Token.CCL_END):
        if not lexer.match(Token.DASH):
            first = lexer.lexeme
            input_set.add(first)
        else:
            lexer.advance()
            for c in range(ord(first), ord(lexer.lexeme) + 1):
                input_set.add(chr(c))
        lexer.advance()


def factor_conn(pair_out):
    if is_conn(lexer.current_token):
        factor(pair_out)
    
    while is_conn(lexer.current_token):
        pair = NfaPair()
        factor(pair)
        pair_out.end_node.next_1 = pair.start_node
        pair_out.end_node = pair.end_node

    return True


def is_conn(token):
    nc = [
        Token.OPEN_PAREN,
        Token.CLOSE_PAREN,
        Token.AT_EOL,
        Token.EOS,
        Token.CLOSURE,
        Token.PLUS_CLOSE,
        Token.CCL_END,
        Token.AT_BOL,
        Token.OR,
    ]
    return token not in nc


def factor(pair_out):
    term(pair_out)
    if lexer.match(Token.CLOSURE):
        nfa_star_closure(pair_out)
    elif lexer.match(Token.PLUS_CLOSE):
        nfa_plus_closure(pair_out)
    elif lexer.match(Token.OPTIONAL):
        nfa_option_closure(pair_out)


def nfa_star_closure(pair_out):
    if not lexer.match(Token.CLOSURE):
        return False
    start = Nfa()
    end = Nfa()
    start.next_1 = pair_out.start_node
    start.next_2 = end

    pair_out.end_node.next_1 = pair_out.start_node
    pair_out.end_node.next_2 = end

    pair_out.start_node = start
    pair_out.end_node = end

    lexer.advance()
    return True


def nfa_plus_closure(pair_out):
    if not lexer.match(Token.PLUS_CLOSE):
        return False
    start = Nfa()
    end = Nfa()
    start.next_1 = pair_out.start_node

    pair_out.end_node.next_1 = pair_out.start_node
    pair_out.end_node.next_2 = end

    pair_out.start_node = start
    pair_out.end_node = end

    lexer.advance()
    return True



def nfa_option_closure(pair_out):
    if not lexer.match(Token.OPTIONAL):
        return False
    start = Nfa()
    end = Nfa()

    start.next_1 = pair_out.start_node
    start.next_2 = end
    pair_out.end_node.next_1 = end

    pair_out.start_node = start
    pair_out.end_node = end

    lexer.advance()
    return True


def expr(pair_out):
    factor_conn(pair_out)
    pair = NfaPair()

    while lexer.match(Token.OR):
        lexer.advance()
        factor_conn(pair)
        start = Nfa()
        start.next_1 = pair.start_node
        start.next_2 = pair_out.start_node
        pair_out.start_node = start

        end = Nfa()
        pair.end_node.next_1 = end
        pair_out.end_node.next_2 = end
        pair_out.end_node = end

    return True


def group(pair_out):
    if lexer.match(Token.OPEN_PAREN):
        lexer.advance()
        expr(pair_out)
        if lexer.match(Token.CLOSE_PAREN):
            lexer.advance()
    elif lexer.match(Token.EOS):
        return False
    else: 
        expr(pair_out)

    while True:
        pair = NfaPair()
        if lexer.match(Token.OPEN_PAREN):
            lexer.advance()
            expr(pair)
            pair_out.end_node.next_1 = pair.start_node
            pair_out.end_node = pair.end_node
            if lexer.match(Token.CLOSE_PAREN):
                lexer.advance()
        elif lexer.match(Token.EOS):
            return False
        else: 
            expr(pair)
            pair_out.end_node.next_1 = pair.start_node
            pair_out.end_node = pair.end_node

##########################PARSE###########################

def match(input_string, nfa_machine):
    start_node = nfa_machine

    current_nfa_set = [start_node]
    next_nfa_set = closure(current_nfa_set)

    for i, ch in enumerate(input_string):
        current_nfa_set = move(next_nfa_set, ch)
        next_nfa_set = closure(current_nfa_set)

        if next_nfa_set is None:
            return False

        if has_accepted_state(next_nfa_set) and i == len(input_string) - 1:
            return True

    return False


def closure(input_set):
    if len(input_set) <= 0:
        return None

    nfa_stack = []
    for i in input_set:
        nfa_stack.append(i)

    while len(nfa_stack) > 0:
        nfa = nfa_stack.pop()
        next1 = nfa.next_1
        next2 = nfa.next_2
        if next1 is not None and nfa.edge == EPSILON:
            if next1 not in input_set:
                input_set.append(next1)
                nfa_stack.append(next1)

        if next2 is not None and nfa.edge == EPSILON:
            if next2 not in input_set:
                input_set.append(next2)
                nfa_stack.append(next2)
        
    return input_set


def move(input_set, ch):
    out_set = []
    for nfa in input_set:
        if nfa.edge == ch or (nfa.edge == CCL and ch in nfa.input_set):
            out_set.append(nfa.next_1)

    return out_set


def has_accepted_state(nfa_set):
    for nfa in nfa_set:
        if nfa.next_1 is None and nfa.next_2 is None:
            return True


##########################REGEX###########################
class Regex(object):
    def __init__(self, input_string, pattern_string):
        self.input_string = input_string
        self.pattern_string = pattern_string

    def match(self):
        pattern_string = self.pattern_string
        input_string = self.input_string
        nfa_machine = _pattern(pattern_string)
        return match(input_string, nfa_machine)


########################FINALIZE########################
def MATCH(string:str,pattern:str):
    words = string.split()
    modulo = 1
    if len(pattern.split()) > 1:
        modulo = len(pattern.split())
    match_count = 0
    match_position = []
    pattern = ".?"+pattern.strip().replace(" ",".?").lower()+".?"
    for i in range(len(words)-modulo+1):
        word = words[i].strip()
        for j in range(1,modulo):
            word += " " + words[i+j].strip()
        if (word == ""):
            return False
        regex = Regex(word.lower(), pattern)
        result = regex.match()
        if result or word.lower().find(pattern.lower()) != -1:
            match_count += 1
            match_position.append(i)


    return match_count

def MATCHBS(string:str,pattern:str):
    words = string.split()
    modulo = 1
    if len(pattern.split()) > 1:
        modulo = len(pattern.split())
    match_count = 0
    match_position = []
    pattern = ".?"+pattern.strip().replace(" ",".?")+".?"
    for i in range(len(words)-modulo+1):
        word = words[i].strip()
        for j in range(1,modulo):
            word += " " + words[i+j].strip()
        if (word == ""):
            return False
        regex = Regex(word, pattern)
        result = regex.match()
        if result or word.find(pattern) != -1:
            match_count += 1
            match_position.append(i)


    return match_count

if __name__ == "__main__":
    #MATCH('AS342abcdefg234aaaaabccccczczxczcasdzxc','([A-Z]+[0-9]*abcdefg)([0-9]*)(\*?|a+)(zx|bc*)([a-z]+|[0-9]*)(asd|fgh)(zxc)')
    print(MATCH('hello koliber hawański wali drzewo','hawański'))
    print(MATCH('hello koliber hawański wali drzewo','koliber hawański'))
