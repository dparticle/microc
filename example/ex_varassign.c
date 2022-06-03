// micro-C example var assign

int glo_i = 1;
// string glo_s = "s1";
int glo_ii = glo_i + 1;

void main() {
    // prints "glo_i: ";
    print glo_i;
    // prints "\nglo_ii: ";
    print glo_ii;
    int loc_i = 3;
    // prints "\nloc_i: ";
    print loc_i;
    (* print (glo_i + 999); *)

    // prints "\nglo_s: ";
    // prints glo_s;
    // int loc_s = "s2";
    // prints "\nloc_s: ";
    // prints loc_s;
}